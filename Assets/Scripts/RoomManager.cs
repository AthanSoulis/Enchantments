using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public bool[] roomsVisitState = null;
	private bool gameEnd = false;

	private void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
	void Start () {
		// if(roomsVisitState == null)
		this.ResetRooms();
	}
	
	void Update () {
		if(roomsVisitState[0] && roomsVisitState[1] && roomsVisitState[2]){
			this.EndGame();
		}
	}
	void EndGame() {
		if(!gameEnd)
			gameEnd = true;
	}
	public bool hasGameEnded(){
		return this.gameEnd;
	}

	public void VisitRoom(int index){
		Debug.Log("Visited Room " + index);
		if(index >= 0)
			this.roomsVisitState[index] = true;
	}

	public void ResetRooms(){
		Debug.Log("Reseting Rooms");
		this.roomsVisitState = new bool[3];
	}

	public bool isRoomVisited(int index){
		return roomsVisitState[index];
	}
}
