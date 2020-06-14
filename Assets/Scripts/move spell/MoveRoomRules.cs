using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoomRules : MonoBehaviour {

	public int SpellsToCast = -1;
	public LoadSceneSpell portal;

	// Use this for initialization
	void Start () {
		this.SpellsToCast = 5;
	}
	
	// Update is called once per frame
	void Update () {
			
			if(SpellsToCast == 0){
				SpellsToCast = -1;

				this.portal.unlock(true);
			}
		
	}
}
