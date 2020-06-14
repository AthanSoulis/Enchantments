using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using AirSig;

public class GestureManager : MonoBehaviour {

    [Tooltip("Reference to AirSigManager for setting operation mode and registering listener.")]
    public AirSigManager airsigManager;
	[Tooltip("Reference to the vive right hand controller for handing key pressing.")]
    public Hand rightHandControl;
	[Tooltip("An array with the gesture signatures that guide the user what gesture to draw.")]
	public GameObject[] gestureCanvasGuide; 
	[Tooltip("The trail the recorded movement leaves.")]
	public float fringeCastTime = 0.1f;
	protected ParticleSystem gestureTrail;
	protected PlaySound spellSoundPlayer;
	protected AudioSource audioSource;
	protected Dictionary<string,RawImage> gestureImages;

	// Set by the callback function to run this action in the next UI call
    protected Action nextUiAction;
    protected IEnumerator uiFeedback;

	protected bool playerCasting = false;
	protected float castTimer = 0.0f;

	// Use this for initialization
	void Start () {
		gestureImages = new Dictionary<string,RawImage>();
		foreach (GameObject item in gestureCanvasGuide)
			gestureImages.Add(item.name, item.GetComponent<RawImage>());

		Debug.Log("GestureImages: "+gestureImages.Count);

		if( this.rightHandControl.GuessCurrentHandType() != Hand.HandType.Right ) {
			this.rightHandControl = this.rightHandControl.otherHand;
		}

		gestureTrail = this.rightHandControl.GetComponentInChildren<ParticleSystem>();
		if(gestureTrail == null)
			Debug.Log("Cannot find Particle System");

		spellSoundPlayer = this.rightHandControl.GetComponentInChildren<PlaySound>();
		if(spellSoundPlayer == null)
			Debug.Log("Cannot find PlaySound on Hand");
		else
			this.audioSource = spellSoundPlayer.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	protected void checkDbExist() {
        if (!airsigManager.IsDbExist) {
    		Debug.LogError("Cannot find the recorded signature Assets!");
        }
    }
	protected void UpdateUIandHandleControl() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }
		
		if(rightHandControl != null && rightHandControl.controller != null){
			if (rightHandControl.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger )) {
				castTimer += Time.deltaTime;

				if(gestureTrail != null) {
					gestureTrail.Clear();
					gestureTrail.Play();
				}
				if(spellSoundPlayer != null) {
					// spellSoundPlayer.Stop();
					spellSoundPlayer.Play(0);
				}
				if(castTimer >= fringeCastTime)
					this.playerCasting = true;
				else
					this.playerCasting = false;
				
			} else if (rightHandControl.controller.GetPressUp( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger )) {
				if(gestureTrail != null)
					gestureTrail.Stop();
				if(spellSoundPlayer != null){
					StartCoroutine(this.stopSound());
					StopCoroutine(this.stopSound());
				}
				castTimer = 0.0f;
			}
		}

        if (nextUiAction != null) {
            nextUiAction();
            nextUiAction = null;
        }
    }

	IEnumerator stopSound() {
		float startVolume = audioSource.volume;
		while (this.audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / 0.5f;
			yield return null;
		}
		spellSoundPlayer.Stop();
		this.audioSource.volume = startVolume;
	}

}
