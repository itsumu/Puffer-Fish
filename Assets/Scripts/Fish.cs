using System.Collections;
using UnityEngine;

public class Fish : MonoBehaviour {
	const int fishIdling = 0;
	const int fishPuffing1 = 1;
	const int fishPuffing2 = 2;
	const int fishPuffing3 = 3;

	Animator fishAnimator;
	public int fishState = 0;
	public int speed = 5;
	public int jumpForce = 4000;

	// Use this for initialization
	void Start () {
		// Move forward right
		Rigidbody2D fishBody;
		fishBody = GetComponent<Rigidbody2D>();
		fishBody.velocity = Vector2.right * speed;

		// Set fish's animation
		fishAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce);
			if (fishState == fishIdling) {
				fishAnimator.Play ("FishPuff1");
				fishState = fishPuffing1;
			} else if (fishState == fishPuffing1) {
				fishAnimator.Play ("FishPuff2");
				fishState = fishPuffing2;
			} else if (fishState == fishPuffing2) {
				fishAnimator.Play ("FishPuff3");
				fishState = fishPuffing3;
			}
		}
	}
}
