using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Valve.VR.InteractionSystem;

public class ActivateObject : MonoBehaviour {

	protected RawImage gestureImage;
	protected DeveloperDefinedGestureManager ddgm;
	void Start () {
		gestureImage = this.GetComponentInChildren<RawImage>();
		ddgm = FindObjectOfType<DeveloperDefinedGestureManager>();
	}
	void Update () {
		
	}
	public void ObjectActivated() {

		MeshRenderer obMeshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();

		if (obMeshRenderer == null) {
			Debug.logger.LogWarning(this.name, "Could not find Mesh Renderer of object.");
			return;
		}

		Debug.logger.Log(this.name, "Activated Object!");
	}
	// Show the Gesture Image the user needs to draw
	public void EnableGestureImage() {
		if(gestureImage)
			if(gestureImage.isActiveAndEnabled){
				gestureImage.enabled = true;
				Debug.Log("Setting Gesture to Validate:|"+gestureImage.gameObject.name+"|");
				ddgm.setGestureToValidate(this, gestureImage.gameObject.name);
			}
	}
	public void DisableGestureImage() {
		if(gestureImage)
			if(gestureImage.isActiveAndEnabled){
				gestureImage.enabled = false;
				// ddgm.setGestureToValidate(null, null);
			}
	}

	public void ToggleGestureImage() {
		if(gestureImage){
			if(gestureImage.isActiveAndEnabled){
				gestureImage.enabled = false;
				ddgm.setGestureToValidate(null, null);
			}
			else{
				gestureImage.enabled = true;
				Debug.Log("Setting Gesture to Validate:|"+gestureImage.gameObject.name+"|");
				ddgm.setGestureToValidate(this, gestureImage.gameObject.name);
			}
		}
	}
	public bool isGestureImageEnabled() {
		if(gestureImage)
			return gestureImage.isActiveAndEnabled;
		return false;
	}

}
