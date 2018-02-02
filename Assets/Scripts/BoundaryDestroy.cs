using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDestroy : MonoBehaviour {
	Transform viewPortTransform;
	BoxCollider2D backgroundCollider;

	// Use this for initialization
	void Start () {
		this.viewPortTransform = GameObject.Find ("Main Camera").transform;
		this.backgroundCollider = GameObject.Find ("Background").GetComponent<BoxCollider2D> ();
	}

	// Update is called once per frame
	void Update () {
		if (isOutOfView ()) {
			Destroy (this.gameObject);
		}
	}

	// Check if the barrier is out of viewport
	bool isOutOfView() {
		float sizeOfBarrier = GetComponent<BoxCollider2D> ().size.x;
		if (viewPortTransform.position.x - this.transform.position.x >
			backgroundCollider.size.x / 2 + sizeOfBarrier / 2) {
			return true;
		}
		return false;
	}

}
