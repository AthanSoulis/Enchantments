using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Valve.VR.InteractionSystem;

public class LoadSceneSpell : Spell {

	public bool DebugMode = false;
	public bool open = true;
	public string sceneToLoad = null;
	public int roomIndex = -1;
	public GameObject portalFX;
	private PlaySound soundPlayer = null;
	private RoomManager roomManager;

	public void Start() {
		this.soundPlayer = this.GetComponent<PlaySound>();
		this.roomManager = (RoomManager)FindObjectOfType(typeof(RoomManager));
		
		string scene = SceneManager.GetActiveScene().name;
		if(scene.Equals("MainRoom", System.StringComparison.Ordinal))
			this.open = !roomManager.isRoomVisited(roomIndex);
		
		this.portalFX.SetActive(open);
	}
	private void Update() {
		if(DebugMode){
			if(roomIndex == 0 && Input.GetKeyDown(KeyCode.Keypad0)){
				hackDoSpell(0, "Room1");
			} else if (roomIndex == 1 && Input.GetKeyDown(KeyCode.Keypad1)) {
				hackDoSpell(1, "RoomG");
			} else if (roomIndex == 2 && Input.GetKeyDown(KeyCode.Keypad2)) {
				hackDoSpell(2, "Room2");
			} else if (roomIndex == -1 && Input.GetKeyDown(KeyCode.Space)) {
				hackDoSpell(-1, "MainRoom");
			}
		}
	}
	private void hackDoSpell(int roomIndex, string sceneToLoad) {
		if(open && sceneToLoad != null){
			this.roomManager.VisitRoom(roomIndex);
			SteamVR_LoadLevel.Begin(sceneToLoad);
		}
		else
			this.soundPlayer.Play(0);
	}
	public override void doSpell(){
		if(open && sceneToLoad != null){
			this.roomManager.VisitRoom(this.roomIndex);
			SteamVR_LoadLevel.Begin(sceneToLoad);
		}
		else
			this.soundPlayer.Play(0);
	}
	public void unlock(bool playSound) {

		this.open = true;
		this.portalFX.SetActive(this.open);
		if(this.soundPlayer && playSound)
			this.soundPlayer.Play(1);
	}
}
