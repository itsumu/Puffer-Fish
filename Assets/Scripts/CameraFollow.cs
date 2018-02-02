using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform focusTarget;

	// Update is called once per frame
	void LateUpdate () {
		if (GameController.instance.gameOver) { // Stop tracing when game is over
			this.enabled = false;
			return;
		}
		transform.position = new Vector3 (focusTarget.position.x,
			transform.position.y,
			transform.position.z);
	}
}
