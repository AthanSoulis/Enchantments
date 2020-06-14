using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;
public class RaycastInput : MonoBehaviour {

	protected Hand handControl;
	protected SteamVR_LaserPointer rayCastLaser;
	protected GameObject hitObject;

	void Start () {
		this.handControl = this.GetComponentInParent<Hand>();
		this.rayCastLaser = this.GetComponent<SteamVR_LaserPointer>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit rayHit;
		// Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rayHit))
		{
			//If the object is the same as the one we hit last frame
			if (hitObject != null && hitObject == rayHit.transform.gameObject)
			{
				//Trigger "Stay" method on Interactable every frame we hit
				RayStay(rayHit);
			}
			//We're still hitting something, but it's a new object
			else
			{
				//Trigger the ray "Exit" method on Interactable
				RayExit();

				//Keep track of new object that we're hitting, and trigger the ray "Enter" method on Interactable
				RayEnter(rayHit);
			}
		}
		else
		{
			//We aren't hitting anything. Trigger ray "Exit" on Interactable
			RayExit();
		}

		//Close the Ray when Casting
		if(handControl != null && handControl.controller!= null){
			if (handControl.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger )) {
				if(this.rayCastLaser.pointer.activeSelf)
					this.closeLaser();
			}

			if (handControl.controller.GetPressUp( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger )) {
				if(!this.rayCastLaser.pointer.activeSelf)
					this.openLaser();
			}
		}
	}

	protected void RayEnter(RaycastHit hit)
	{	
		hitObject = hit.transform.gameObject;
		Debug.Log("RayEnter");
		InteractableHoverEvents hoverInteractableObj = hitObject.GetComponent<InteractableHoverEvents>();
		if(hoverInteractableObj){
			this.handControl.controller.TriggerHapticPulse( 500 );
			hoverInteractableObj.onHandHoverBegin.Invoke();
		}
	}

	protected void RayStay(RaycastHit hit)
	{
		Debug.Log("RayStay");
		if(handControl != null && handControl.controller != null){
			if (handControl.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger )) {
				LoadSceneSpell loadScene = hitObject.GetComponent<LoadSceneSpell>();
				if(loadScene){
					loadScene.doSpell();
					return;
				}
				QuitSpell quit = hitObject.GetComponent<QuitSpell>();
				if(quit){
					quit.doSpell();
					return;
				}
				ActivateObject objForActivation = hitObject.GetComponent<ActivateObject>();
				if(objForActivation)
					hitObject.GetComponent<ActivateObject>().ToggleGestureImage();
			}
		}
	}

	protected void RayExit()
	{
		if(hitObject) {
			Debug.Log("RayExit");
			InteractableHoverEvents hoverInteractableObj = hitObject.GetComponent<InteractableHoverEvents>();
			if(hoverInteractableObj)
				hoverInteractableObj.onHandHoverEnd.Invoke();
			hitObject = null;
		}
	}

	protected void closeLaser(){
		this.rayCastLaser.pointer.SetActive(false);
	}

	protected void openLaser() {
		this.rayCastLaser.pointer.SetActive(true);
	}
}
