using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
	public Transform viewport;
	private BoxCollider2D backgroundCollider;
	private float backgroundLength;

	// Use this for initialization
	void Start () {
		backgroundCollider = GetComponent<BoxCollider2D> ();
		backgroundLength = backgroundCollider.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (viewport.position.x - transform.position.x > backgroundLength) { // Background out of view, scroll it
			RepositionBackground();
		}
	}

	// Reposition background when it's out of view
	void RepositionBackground() {
		Vector2 movingOffset = new Vector2 (backgroundLength * 2f, 0);
		transform.position = (Vector2)transform.position + movingOffset;
	}
}
