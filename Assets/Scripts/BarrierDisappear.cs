using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDisappear : MonoBehaviour {
	public Transform viewPortTransform;
	public BoxCollider2D backgroundCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isOutOfView ()) {
			Destroy (this.gameObject);
		}
	}

	bool isOutOfView() {
		if (viewPortTransform.position.x - this.transform.position.x > backgroundCollider.size.x) {
			return true;
		}
		return false;
	}
}
