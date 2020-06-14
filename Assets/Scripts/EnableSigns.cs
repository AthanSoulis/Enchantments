using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSigns : MonoBehaviour {

	private RoomManager roomManager;
	public GameObject[] signs;
	private bool done = false;
	void Start () {
		this.roomManager = (RoomManager)FindObjectOfType(typeof(RoomManager));
		if(roomManager.hasGameEnded())
			activateSigns();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void activateSigns() {
		if(!done) {
			foreach (GameObject sign in signs)
				sign.SetActive(true);

			done = true;
		}
	}
}
