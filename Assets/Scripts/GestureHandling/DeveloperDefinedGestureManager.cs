using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;
using AirSig;
public class DeveloperDefinedGestureManager : GestureManager {

	// Callback for receiving signature/gesture progression or identification results
    AirSigManager.OnDeveloperDefinedMatch developerDefined;
	// protected Stack<string> gestureToValidate = null;
	// protected Stack<ActivateObject> activeObject = null;
	protected string gestureToValidate = null;
	protected ActivateObject activeObject = null;
	protected bool CorrectSpellGuard = false;
	protected bool IncorrectSpellGuard = false;

	public void setGestureToValidate(ActivateObject activateObject, string gesture) {
		if(activateObject != null && gesture != null){
			
			if(this.activeObject != null && this.activeObject.isGestureImageEnabled()){
				// User selected an object while another was active.
				// Forget the last and remember the new one.
				if(!activateObject.Equals(this.activeObject)){
					this.activeObject.ToggleGestureImage();
					Debug.Log(string.Format("Gesture to Validate: {0}", gesture));
					this.activeObject = activateObject;
					this.gestureToValidate = gesture;
				}
			}
			else {
				Debug.Log(string.Format("Gesture to Valideate: {0}", gesture));
				this.activeObject = activateObject;
				this.gestureToValidate = gesture;
			}
		}
		else {
			// User selected the same object.
			// We are then forgetting the active object. 
			// ToggleGestureImage() will be called through raycasting
			Debug.Log(string.Format("Forgot Gesture to Validate: {0}", gesture));
			this.activeObject = null;
			this.gestureToValidate = null;
		}
	}

    // Handling developer defined gesture match callback - This is invoked when the Mode is set to Mode.DeveloperDefined and a gesture is recorded.
    // gestureId - a serial number
    // gesture - gesture matched or null if no match. Only guesture in SetDeveloperDefinedTarget range will be verified against
    // score - the confidence level of this identification. Above 1 is generally considered a match
    void HandleOnDeveloperDefinedMatch(long gestureId, string gesture, float score) {
		
		if(this.activeObject != null && this.gestureToValidate != null){
			Debug.Log(string.Format("Gesture Match: {0} Score: {1} GestureToValidate: {2}", gesture.Trim(), score, this.gestureToValidate));
			//Gesture Validated
			if(score >= 1.0 && gestureToValidate == gesture.Trim()){
				Debug.Log("Correct Spell !");
				CorrectSpellGuard = true;			
			}
			else
			//Gesture was not valid
				IncorrectSpellGuard = true;
		}
    }

	protected void correctSpellHandler() {
		// this.gestureToValidate.Pop();
		// ActivateObject spellObject = this.activeObject.Pop();
		ActivateObject spellObject = this.activeObject;
		Spell spellToCast = spellObject.GetComponent<Spell>();
		spellObject.ToggleGestureImage();
		if(spellToCast)
			spellToCast.doSpell();

		float minPitch = this.spellSoundPlayer.pitchMin;
		float maxPitch = this.spellSoundPlayer.pitchMax;

		this.spellSoundPlayer.pitchMax = 1;
		this.spellSoundPlayer.pitchMin = 1;
		this.spellSoundPlayer.Play(1);

		this.spellSoundPlayer.pitchMax = maxPitch;
		this.spellSoundPlayer.pitchMin = minPitch;
		// Reset guards
		this.CorrectSpellGuard = false;
		this.playerCasting = false;
	}

	protected void incorrectSpellHandler() {
		Debug.Log("Incorrect Spell !");
		if(this.playerCasting){
			float minPitch = this.spellSoundPlayer.pitchMin;
			float maxPitch = this.spellSoundPlayer.pitchMax;

			this.spellSoundPlayer.pitchMax = 1;
			this.spellSoundPlayer.pitchMin = 1;
			this.spellSoundPlayer.Play(2);

			this.spellSoundPlayer.pitchMax = maxPitch;
			this.spellSoundPlayer.pitchMin = minPitch;
		}
		// Reset guards
		this.IncorrectSpellGuard = false;
		this.playerCasting = false;
	}

	// Use this for initialization
	void Awake () {
		Debug.Log(string.Format("Mode: {0}", AirSigManager.Mode.DeveloperDefined.ToString()));

		// Configure AirSig by specifying target 
        developerDefined = new AirSigManager.OnDeveloperDefinedMatch(HandleOnDeveloperDefinedMatch);
        airsigManager.onDeveloperDefinedMatch += developerDefined;
        airsigManager.SetMode(AirSigManager.Mode.DeveloperDefined);
		List<string> signatureNames = new List<string>();
		foreach (GameObject gestureImage in gestureCanvasGuide) {
			signatureNames.Add(gestureImage.name);
			Debug.Log("Added " + gestureImage.name);
		}

		airsigManager.SetDeveloperDefinedTarget(signatureNames);
        airsigManager.SetClassifier("Enchantments Gesture Profile_1549472589974", "");
        checkDbExist();

		// Create gesture & activeObjects stacks
		// this.gestureToValidate = new Stack<string>();
		// this.activeObject = new Stack<ActivateObject>();

        airsigManager.SetTriggerStartKeys(
            AirSigManager.Controller.RIGHT_HAND,
            SteamVR_Controller.ButtonMask.Trigger,
            AirSigManager.PressOrTouch.PRESS);
		
		if(this.rightHandControl == null)
			this.rightHandControl = FindObjectOfType<Player>().GetComponentInChildren<Hand>();
	}
	void onDestroy() {
		// Unregistering callback
        airsigManager.onDeveloperDefinedMatch -= developerDefined;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateUIandHandleControl();

		if(CorrectSpellGuard)
			this.correctSpellHandler();
		if(IncorrectSpellGuard)
			this.incorrectSpellHandler();
	}
}
