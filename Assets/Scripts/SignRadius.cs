using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(PlaySound))]
[RequireComponent(typeof(CapsuleCollider))]
public class SignRadius : MonoBehaviour {
	private PlaySound sound;
	private Collider collider;
	private Outline outline = null;
	private Coroutine blinking = null;
	// Use this for initialization
	void Start () {
		this.sound = GetComponent<PlaySound>();
		this.collider = GetComponent<Collider>();
		this.outline = GetComponent<Outline>();
		this.collider.isTrigger = true;
		this.blinking = StartCoroutine(blinkingEnumerator());
	}

	
	private void OnTriggerEnter(Collider other) {
		this.sound.Stop();
		StopCoroutine(this.blinking);
		this.outline.enabled = false;
		this.collider.enabled = false;
	}

	IEnumerator blinkingEnumerator() {
		while(outline != null) {

			this.outline.enabled = !this.outline.enabled;
			yield return new WaitForSeconds( 1.0f );
		}
	}
}
