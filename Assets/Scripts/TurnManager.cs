using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the game manager, this script manages troop turn order

public class TurnManager : MonoBehaviour {

	// The number of players, can be edited from the editor screen or other scripts
	//public int numberOfPlayers = 2;

	// An array that can hold all players
	//public playerController[] playerList;

	public GameManager gameManager;
	public playerController mover;

	// If the turns are playing or not
	bool going = false;

	// If the script is waiting for a player character to finish moving
	//bool waiting = false;

	bool finished = true;

	// Keeps track of which player is taking their action
	//int currentPlayer = 0;

	void Start () {
		// This puts all players in the playerList array, as long as they're named in the format of "PlayerX"
		//playerList = new playerController[numberOfPlayers];
		/*for (int i = 0; i < numberOfPlayers; i++) {
			playerList [i] = GameObject.Find ("Player" + (i + 1)).GetComponent (typeof(playerController)) as playerController;
		}*/
		gameManager = GetComponent<GameManager> ();
	}

	void Update () {
		if (Input.GetKeyDown ("space") && !going) {
			going = true;
		} 
		if (going) {
			if (finished) {
				if (gameManager.moveQueue.Count == 0) {
					going = false;
					return;
				}
				GameManager.Move move = gameManager.moveQueue.Dequeue ();
				if (move.player == null)
					return;
				mover = move.player.GetComponent<playerController> ();
				move.execute ();
				if (!move.isAttack)
					finished = false;
			} else {
				if (!mover.moving)
					finished = true;
			}
		}
	}
}