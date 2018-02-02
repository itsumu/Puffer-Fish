using System.Collections;
using UnityEngine;

public class Fish : MonoBehaviour {
	const int fishIdling = 0;
	const int fishPuffing1 = 1;
	const int fishPuffing2 = 2;
	const int fishPuffing3 = 3;

	Rigidbody2D rigidBody;
	Animator fishAnimator;
	public int actionState = 0;
	public int shiftSpeed = 50;
	public int jumpForce = 5000;
	public float swimmingAltitude;

	// Use this for initialization
	void Start () {
		this.rigidBody = GetComponent<Rigidbody2D>();
		this.rigidBody.gravityScale = 0; // Keep fish swimming horizontally
		this.rigidBody.velocity = Vector2.right * shiftSpeed; // Move forward right
		this.fishAnimator = GetComponent<Animator>(); // Set fish's animation
		this.swimmingAltitude = this.transform.position.y; // Fish's swimming altitude
	}
	
	// Update is called once per frame
	void Update () {
		if (this.actionState != fishIdling && this.transform.position.y <= this.swimmingAltitude) { 
			// Fish enter water
			ForceIdling ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) { // Key pressed
			TriggerStateChange ();
		}
	}

	// Key pressed, trigger changing of fish's action state
	void TriggerStateChange() {
		if (actionState == fishIdling) {
			this.fishAnimator.Play ("FishPuff1");
			this.actionState = fishPuffing1;
			this.rigidBody.gravityScale = 1; // Fish can feel gravity now ~
			this.rigidBody.AddForce (Vector2.up * jumpForce); // Take a jump
		} else if (actionState == fishPuffing1) {
			this.fishAnimator.Play ("FishPuff2");
			this.actionState = fishPuffing2;
		} else if (actionState == fishPuffing2) {
			this.fishAnimator.Play ("FishPuff3");
			this.actionState = fishPuffing3;
		}
	}

	// Force the fish to idle when it enter the water
	void ForceIdling() {
		this.rigidBody.velocity = new Vector2(shiftSpeed, 0); // Stop dropping
		this.rigidBody.gravityScale = 0;
		this.fishAnimator.Play ("FishIdle");
		this.actionState = fishIdling;
	}

	// Check collision between fish & bubble
	void OnTriggerStay2D(Collider2D bubble) {
		if (Input.GetKeyDown(KeyCode.Space)) { // Fish jump & destroy bubble when key pressed
			if (this.actionState != fishPuffing3) { // Fish in state puffing3 can't puff any more
				this.rigidBody.velocity = new Vector2 (shiftSpeed, 0); // Stop speed in y axis to prevent it flying away
				this.rigidBody.AddForce (Vector2.up * jumpForce); // Take a jump
				Destroy (bubble.gameObject);
			}
		}
	}

	// Check collision between fish & barrier
	void OnCollisionStay2D(Collision2D barrier) {
		RestartGame ();
	}

	void RestartGame() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (
			UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
	}
}
