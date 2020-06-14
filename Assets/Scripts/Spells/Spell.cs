using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

[RequireComponent(typeof(InteractableHoverEvents))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(ActivateObject))]
[RequireComponent(typeof(Outline))]

public class Spell : MonoBehaviour {

	private InteractableHoverEvents hoverEvents;
	private Interactable interactable;
	private ActivateObject activateObject;
	private Outline outline;

	public void Awake() {
		hoverEvents = this.GetComponent<InteractableHoverEvents>();
		interactable = this.GetComponent<Interactable>();
		activateObject = this.GetComponent<ActivateObject>();
		outline = this.GetComponent<Outline>();

		hoverEvents.onHandHoverBegin.AddListener(() =>{ outline.enabled = true; });
		hoverEvents.onHandHoverEnd.AddListener(() => { outline.enabled = false; });
		this.outline.enabled = false;
		Debug.Log("Outline: "+ this.outline.enabled);

	}
	public virtual void doSpell(){}
}
