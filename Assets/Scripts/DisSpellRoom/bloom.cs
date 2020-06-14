using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloom : MonoBehaviour {
     AudioSource source;
    
	// Use this for initialization
	void Start () {
        source = gameObject.GetComponent<AudioSource>();
	}

    private void OnTriggerEnter(Collider other)
    {
        source.Play(0);
    }
}

