using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem;
public class Tutorial : MonoBehaviour {

	public LoadSceneSpell[] portalDoors;
	public int SpellsToCast = -1;
	public Hand hand;
	private RoomManager roomManager;
	private Coroutine buttonHintCoroutine;

	void Start() {
		// Start tutorial
		Debug.Log("In tutorial!");
		foreach (LoadSceneSpell portalDoor in portalDoors)
			portalDoor.open = false;

		if ( buttonHintCoroutine != null )
		{
			StopCoroutine( buttonHintCoroutine );
		}
		buttonHintCoroutine = StartCoroutine( TestButtonHints( hand ) );
	}
	
	// Update is called once per frame
	void Update () {
		if(SpellsToCast == 0){
			SpellsToCast = -1;
			for (int i = 0; i < portalDoors.Length; i++)
				portalDoors[i].unlock(i==1);
		}

		if (hand != null && hand.controller != null && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger )){
			StopCoroutine(buttonHintCoroutine);
			ControllerButtonHints.HideAllTextHints( hand );
			ControllerButtonHints.HideAllButtonHints( hand );
		}

	}

	private IEnumerator TestButtonHints( Hand hand )
	{
		ControllerButtonHints.HideAllButtonHints( hand );
		while ( true )
		{
			ControllerButtonHints.ShowTextHint( hand, EVRButtonId.k_EButton_SteamVR_Trigger, "Select item / Hold for spell gesture" );
			yield return new WaitForSeconds( 1.0f );
		}
	}
}
