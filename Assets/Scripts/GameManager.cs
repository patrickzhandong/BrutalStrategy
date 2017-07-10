using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public BoardManager boardScript;
	public GameObject team1Player;
	public GameObject team2Player;
	private GameObject target;
	private bool playerMoved = false;
	private bool playerAttacked = false;
	private bool playerKilled = false;
	private RaycastHit2D hit;
	private GameObject playerAtTurn;

	[Serializable]
	public class Move
	{
		public bool isAttack;
		public GameObject player;
		public Vector3 movePos;
		public GameObject target;

		public Move (bool attack, GameObject player)
		{
			isAttack = attack;
			this.player = player;
		}

		public void execute ()
		{
			if (!isAttack) {
				playerController mover = player.GetComponent<playerController> ();
				mover.endPoint = movePos;
				mover.moving = true;
			} else {
				if (target == null) {
					Debug.Log ("attack is missed");
				} else {
					Vector3 pos = target.transform.position;
					playerController mover = player.GetComponent<playerController> ();
					mover.attackPos = new Vector2 (pos.x, pos.y);
					mover.attacking = true;
				}
			}
		}
	}

	public Queue<Move> moveQueue = new Queue<Move> ();

	// Use this for initialization
	void Awake () {
		boardScript = GetComponent<BoardManager>();
		playerAtTurn = GameObject.FindGameObjectWithTag ("Player1");
		InitGame();

	}

	void InitGame()
	{
		MapManager.getMapManager().initialize();
		boardScript.SetupScene(MapManager.getMapManager().getMapByIndex(0));
	}

	// Update is called once per frame
	void Update () {

		Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

		if (Input.GetButtonDown ("Fire1") && !playerMoved && !playerAttacked) 
		{
			hit = Physics2D.Raycast (rayPos, Vector2.zero, 0f);
			if (!hit) {
				playerController mover = playerAtTurn.GetComponent<playerController> ();
				if (Mathf.Abs (rayPos.x) <= 2.5f && Mathf.Abs (rayPos.y) <= 2.5f  && !mover.moving) {
					Move move = new Move (false, playerAtTurn);
					move.movePos = new Vector3 (rayPos.x, rayPos.y, 0f);
					moveQueue.Enqueue (move);
					//mover.moving = true;
					playerMoved = true;
				}
			}
		} 

		else if (Input.GetButtonDown ("Fire1") && playerMoved && !playerAttacked) 
		{
			if (Mathf.Abs (rayPos.x) > 2.5f || Mathf.Abs (rayPos.y) > 2.5f)
				return;

			Move move = new Move (true, playerAtTurn);
			RaycastHit2D hit = Physics2D.Raycast (rayPos, Vector2.zero, 0f);
			if (hit) {
				move.target = GameObject.FindGameObjectWithTag (hit.transform.name);
			} else {
				move.target = null;
			}

			moveQueue.Enqueue (move);
			playerAttacked = true;
		} 

		if (playerAttacked && playerMoved && !playerKilled) 
		{
			//switch turns
			if (playerAtTurn.transform.name == "Player1")
				playerAtTurn = GameObject.FindGameObjectWithTag ("Player2");
			else
				playerAtTurn = GameObject.FindGameObjectWithTag ("Player1");
			playerAttacked = false;
			playerMoved = false;
		} 

	}
}
