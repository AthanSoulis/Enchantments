using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisSpellRoomRules : MonoBehaviour {
    public LoadSceneSpell portal;
    public GameObject destrobj;
    private bool locked = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (destrobj == null && locked)
        {
            locked = false;
            this.portal.unlock(true);
        }
    }
}
