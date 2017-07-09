using System.Collections;
using System.Collections.Generic;
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
					mover.endPoint = new Vector3 (rayPos.x, rayPos.y, 0f);
					//mover.moving = true;
					playerMoved = true;
				}
			}
		} 

		else if (Input.GetButtonDown ("Fire1") && playerMoved && !playerAttacked) 
		{
			string attackTag = playerAtTurn.transform.name + "Hp";
			Debug.Log (playerAtTurn.transform.name + "starts to attack");

			if (Mathf.Abs (rayPos.x) > 2.5f || Mathf.Abs (rayPos.y) > 2.5f)
				return;
			
			hit = Physics2D.Raycast (rayPos, Vector2.zero, 0f);
			if (hit) {
				string attackedTag = hit.transform.name + "Hp";
				Debug.Log (attackedTag + "being attacked");

				GameObject attackedTargetHp = GameObject.FindGameObjectWithTag (attackedTag);
				GameObject attackedTarget = GameObject.FindGameObjectWithTag (hit.transform.name);
				HealthBarHeartsWhole attackedHp = attackedTargetHp.GetComponent<HealthBarHeartsWhole> ();
				attackedHp.currentHp -= 2;
				if (attackedHp.currentHp <= 0) {
					Debug.Log (attackedTag + " is killed");
					Destroy (attackedTarget);
					Destroy (attackedTargetHp);
					playerKilled = true;
				}
			} 
			else {
				Debug.Log ("attack is missed");
			}

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
