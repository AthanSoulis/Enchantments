using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hittingFloorSoundEffect : MonoBehaviour {
    AudioSource source;
    private bool playedOnce = false;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!playedOnce ){
            source.Play(0);
            playedOnce = true;
            
        }
            
    }
}

