using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public bool moving = false;
	private float speed = 4f;
	public Vector3 endPoint;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (moving)
			Move (endPoint);
	}

	public void Move (Vector3 end) {
		//transform.position = new Vector3 (0f, 0f, 0f);
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		if (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (transform.position, end, speed * Time.deltaTime);
			transform.position = newPosition;
		} else {
			moving = false;
		}
	}
}
