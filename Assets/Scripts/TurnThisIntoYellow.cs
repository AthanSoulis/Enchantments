using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;
public class TurnThisIntoYellow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TurnObjectIntoYellow() {
		MeshRenderer obMeshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();

		if (obMeshRenderer == null) {
			Debug.logger.LogWarning(this.name, "Could not find Mesh Renderer of object.");
			return;
		}
		obMeshRenderer.material.color = Color.yellow;
	}
}
