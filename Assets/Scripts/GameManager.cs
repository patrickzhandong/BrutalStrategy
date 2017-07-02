using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public BoardManager boardScript;
    public GameObject team1Player;
    public GameObject team2Player;
    private GameObject target;
    private bool playerMoving;
	// Use this for initialization
	void Awake () {
        boardScript = GetComponent<BoardManager>();
        playerMoving = false;
        InitGame();
		
	}
    void InitGame()
    {
        //boardScript.SetupScene();
        
    }
    // Update is called once per frame
    void Update () {
    

        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
            if (hit)
            {
                if (!playerMoving)
                {
                    string attackTag = hit.transform.name + "Hp";
                    Debug.Log(hit.transform.name + "starts to attack");
                    playerMoving = true;
                }
                else
                {
                    playerMoving = false;
                            string attackedTag = hit.transform.name + "Hp";
                            Debug.Log(attackedTag + "being attacked");
                            GameObject attackTargetHp = GameObject.FindGameObjectWithTag(attackedTag);
                            GameObject attackTarget = GameObject.FindGameObjectWithTag(hit.transform.name);
                            HealthBarHeartsWhole attackHp = attackTargetHp.GetComponent<HealthBarHeartsWhole>();
                            attackHp.currentHp -= 2;
                            if (attackHp.currentHp <= 0)
                            {
                                Destroy(attackTarget);
                                Destroy(attackTargetHp);
                            }
                }
               
            }
        }
    }
}
