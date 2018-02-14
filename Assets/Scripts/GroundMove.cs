using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour {
	public int shiftSpeed = 50;
	public Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		this.rigidBody = GetComponent<Rigidbody2D> ();
		this.rigidBody.velocity = Vector2.right * shiftSpeed; // Move forward right
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.instance.gameOver) { // Stop moving when game is over
			this.rigidBody.velocity = Vector2.zero;
		}
	}
}
