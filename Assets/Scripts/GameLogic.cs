/* Basic Server and Client Example
 * 
 *		This is a simple demonstration of the library in action.  It is intended to show simple usage of 
 *		caching images and sounds on the HTML5 client, sending network messages from the server
 *		to clients, handling server messages on clients, sending messages from clients to the server,
 *		and handling client messages on the server, and simple Unity and Phaser usage.
 *		
 *		The high-level functionality will look as follows: there is a night sky displayed on the
 *		streamer's screen, and on all HTML5 clients as well.
 *		
 *		When the streamer clicks their mouse anywhere on their screen, a firework will explode
 *		in that location on all clients screens.  And if any client taps or clicks their screen, a firework
 *		will explode on the streamer's screen in that location.
 */

using System;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    // These two structs are the parameters for the network messages.  They'll be serialized into JSON and then trasmitted across Twitch's IRC.
    // Note that these fields need the [Serializable] attribute.
    // These need to stay in sync (by both type and name) with the Typescript code.

    [Serializable]
	struct ServerFirework {
		public int x;
		public int y;
	}

	[Serializable]
	struct ClientFirework {
       public Vector2 clickPos;
        public String attackTarget;
       public String playerID;
	}


	// These are normal Unity Gameplay elements

	public Transform camera;


	// This is the monobehavior the takes care of all IRC networking.

	public TwitchNetworking networking;

	// This is the interface for sending and recieving network messages.
        
	APG.APGSys apg;


	// This is a function for recieiving network messages from clients.
	// The logic for this will work as follows: when any client taps their HTML5 client or clicks
	// their mouse over it, we will show a firework briefly on the streamer's screen in
	// the location of the tap.
    bool InRange(GameObject playerAtTurn, GameObject playerAttacked)
    {
        return (Math.Abs(playerAtTurn.transform.position.x - playerAttacked.transform.position.x) <= 2f && Math.Abs(playerAtTurn.transform.position.y - playerAttacked.transform.position.y) <= 2f);
    }
    void ClientFireWorkHandler(string sender, ClientFirework data) {
        //AudioSource.PlayClipAtPoint(boomSound, camera.position, 1);
        //firework.transform.position = new Vector3(data.x, 450-data.y, 0);
        //firework.transform.localScale = new Vector3(64,64,1);
        GameObject playerAtTurn = GameObject.FindGameObjectWithTag(data.playerID);
        playerAtTurn.transform.position = new Vector3(playerAtTurn.transform.position.x + data.clickPos.x, playerAtTurn.transform.position.y + data.clickPos.y, 0f);
        GameObject playerAttacked = GameObject.FindGameObjectWithTag(data.attackTarget);
        if (InRange(playerAtTurn, playerAttacked)) {
            string attackedTag = playerAttacked.transform.name + "Hp";
            Debug.Log(attackedTag + "being attacked");

            GameObject attackedTargetHp = GameObject.FindGameObjectWithTag(attackedTag);
            HealthBarHeartsWhole attackedHp = attackedTargetHp.GetComponent<HealthBarHeartsWhole>();
            attackedHp.currentHp -= 2;
            if (attackedHp.currentHp <= 0)
            {
                Debug.Log(attackedTag + " is killed");
                Destroy(playerAttacked);
                Destroy(attackedTargetHp);
            }
        }
    }


    

	void Start () {
		Application.runInBackground = true;

		// We call the following function to get the audience networking interface from our networking component.

		apg = networking.GetAudienceSys();

		// Make sure there are no registered network messages before we add ours.

		apg.ResetClientMessageRegistry();

		// And now register this client message.  This is how the server knows which messages to expect.

		apg.Register<ClientFirework>("clientFirework", ClientFireWorkHandler );
	}

	// Our update is pretty simple.  If the streamer clicks anywhere in their playfield, send a server message
	// down to clients to display a firework on their client screens in that location.

	void Update () {
		if (Input.GetMouseButtonDown(0)) {

			// build the message, and then send it

			ServerFirework data = new ServerFirework { x = (int)Input.mousePosition.x, y = 450 - (int)Input.mousePosition.y };
			apg.WriteToClients<ServerFirework>("serverFirework", data );
			Debug.Log("Write server firework message at point " + data.ToString());
		}
	}
}
