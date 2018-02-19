using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyCloud : MonoBehaviour {
	public int shiftSpeed;
	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.velocity = Vector2.right * shiftSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
