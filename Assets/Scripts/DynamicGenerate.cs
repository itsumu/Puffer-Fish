using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGenerate : MonoBehaviour {
	const float barriersGap = 150;
	const float bubblesGap = 50;
	const float bubblesMinGap = 40;
	const float bubblesHeightInterval = 25f;
	const float maxHeightOfBubble = 25;
	const float minHeightOfBubble = 0;

	public GameObject bubblePrefab;
	public GameObject barrierPrefab;
	public Camera mainCamera;
	Vector3 bubbleOffset;
	Vector3 barrierOffset;
	float lastPosXOfBarrier;
	float lastPosXOfBubble;
	float lastHeightOfBubble;

	// Create a bubble object
	void CreateBubble() {
		Vector3 currentPosition = transform.position;
		currentPosition.z = bubblePrefab.transform.position.z; // Take prefab's z axis
		float randomOffset = Random.Range (0, 50);
		if (lastHeightOfBubble == -1) { // Bubble never exists
			lastHeightOfBubble = minHeightOfBubble;
		} else {
			if (randomOffset < 20) {
				if (lastHeightOfBubble != maxHeightOfBubble) {
					lastHeightOfBubble += bubblesHeightInterval;
				}
			} else {
				lastHeightOfBubble = minHeightOfBubble;
			}
		}
		Vector3 extraOffset = new Vector3(1, 0) * randomOffset + new Vector3(0, 1) * lastHeightOfBubble;
		Instantiate (bubblePrefab, currentPosition + bubbleOffset + extraOffset, Quaternion.identity);
		lastPosXOfBubble = transform.position.x;
	}

	// Create a barrier object
	void CreateBarrier() {
		Vector3 currentPosition = transform.position;
		currentPosition.z = barrierPrefab.transform.position.z; // Take prefab's z axis
		Vector3 extraOffset = new Vector3 (1, 0) * Random.Range (50, 100);
		Instantiate (barrierPrefab, currentPosition + barrierOffset + extraOffset, Quaternion.identity);
		if (transform.position.x - lastPosXOfBubble >= bubblesMinGap) { 
			// Create an extra bubble to make barrier available to jump over
			CreateBubble ();
		}
		lastPosXOfBarrier = transform.position.x;
	}

	// Use this for initialization
	void Start () {
		Random.InitState (100);
		mainCamera = FindObjectOfType<Camera> ();
		bubbleOffset = new Vector3 (mainCamera.pixelWidth / 2, bubblePrefab.transform.position.y);
		barrierOffset = new Vector3(mainCamera.pixelWidth / 2, barrierPrefab.transform.position.y);
		lastPosXOfBarrier = -1000;
		lastPosXOfBubble = -1000;
		lastHeightOfBubble = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x - lastPosXOfBarrier >= barriersGap) {
			CreateBarrier ();
		}
		if (transform.position.x - lastPosXOfBubble >= bubblesGap) {
			CreateBubble ();
		}
	}
}
