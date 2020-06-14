using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureCanvasVisibility : MonoBehaviour {

	public GameObject target;
 	public float maxDistance=10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dirFromHosttoTarget = (target.transform.position - this.transform.position).normalized;
		float dotProd = Vector3.Dot(dirFromHosttoTarget, this.transform.forward);
 
		if(dotProd > 0.9) {
			//Looking at canvas
			target.SetActive(true);
		}else {
			//Not looking at canvas
			if(target.activeSelf)
				target.SetActive(false);
		}
	}
}
