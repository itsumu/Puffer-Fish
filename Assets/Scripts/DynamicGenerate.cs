using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGenerate : MonoBehaviour {
	public GameObject bubblePrefab;
	public GameObject barrierPrefab;
	Vector3 bubbleOffset;
	Vector3 barrierOffset;

	void CreateBubble() {
		Vector3 currentPosition = transform.position;
		Instantiate (bubblePrefab, currentPosition + bubbleOffset, Quaternion.identity);
	}

	void CreateBarrier() {
		Vector3 currentPosition = transform.position;
		Instantiate (barrierPrefab, currentPosition + barrierOffset, Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
		bubbleOffset = bubblePrefab.transform.position;
		barrierOffset = barrierPrefab.transform.position;
		InvokeRepeating ("CreateBubble", 1.0f, 1.5f);
		InvokeRepeating ("CreateBarrier", 1.0f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
