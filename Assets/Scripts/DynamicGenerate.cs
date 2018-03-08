using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGenerate : MonoBehaviour {
	const float barriersGap = 200;
	const float bubblesGap = 50;
	const float bubblesMinGap = 30;
	const float cloudsGap = 150;
	const float greyCloudsGap = 200;
	const float bubblesHeightInterval = 25f;
	const float maxHeightOfBubble = 35;
	const float minHeightOfBubble = 0;

	public GameObject bubblePrefab;
	public GameObject barrierPrefab;
	public GameObject cloudPrefab;
	public GameObject greyCloudPrefab;
	public Camera mainCamera;
	Vector3 bubbleOffset;
	Vector3 barrierOffset;
	Vector3 cloudOffset;
	Vector3 greyCloudOffset;
	float lastPosXOfBarrier;
	float lastPosXOfBubble;
	float lastHeightOfBubble;
	float lastPosXOfCloud;
	float lastPosXOfGreyCloud;

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

	// Being voked only when next bubble is unreachable to help cross barrier
	void CreateExtraBubble() {
		Vector3 currentPosition = transform.position;
		currentPosition.z = bubblePrefab.transform.position.z; // Take prefab's z axis
		float randomOffset = Random.Range(0, 10); // Put bubble forward to be reachable
		lastHeightOfBubble = minHeightOfBubble; // Set bubble's position with the lowest height
		Vector3 extraOffset = new Vector3(1, 0) * randomOffset + new Vector3(0, 1) * lastHeightOfBubble;
		Instantiate (bubblePrefab, currentPosition + bubbleOffset + extraOffset, Quaternion.identity);
		lastPosXOfBubble = transform.position.x;
	}

	// Create a barrier object
	void CreateBarrier() {
		if (transform.position.x - lastPosXOfBubble >= bubblesMinGap) { 
			// Create an extra bubble to make barrier available to jump over
			CreateExtraBubble ();
		}
		Vector3 currentPosition = transform.position;
		currentPosition.z = barrierPrefab.transform.position.z; // Take prefab's z axis
		Vector3 extraOffset = new Vector3 (1, 0) * Random.Range (50, 100);
		Instantiate (barrierPrefab, currentPosition + barrierOffset + extraOffset, Quaternion.identity);
		lastPosXOfBarrier = transform.position.x;
	}

	// Create a cloud & make it move
	void CreateCloud() {
		Vector3 currentPosition = transform.position;
		currentPosition.z = cloudPrefab.transform.position.z; // Take prefab's z axis
		Vector3 extraOffset = new Vector3 (1, 0) * Random.Range (0, 50) +
		                      new Vector3 (0, 1) * Random.Range (-10, 10);
		Instantiate (cloudPrefab, currentPosition + cloudOffset + extraOffset, Quaternion.identity); // New cloud generation
		lastPosXOfCloud = transform.position.x;
	}

	// Create a grey cloud & make it move
	void CreateGreyCloud() {
		Vector3 currentPosition = transform.position;
		currentPosition.z = greyCloudPrefab.transform.position.z; // Take prefab's z axis
		Vector3 extraOffset = new Vector3 (1, 0) * Random.Range (0, 50) + 
							  new Vector3(0, 1) * Random.Range(-10, 10);
		Instantiate (greyCloudPrefab, currentPosition + greyCloudOffset + extraOffset, Quaternion.identity); // New grey cloud generation
		lastPosXOfGreyCloud = transform.position.x;
	}

	// Use this for initialization
	void Start () {
		Random.InitState (100);
		mainCamera = FindObjectOfType<Camera> ();
		bubbleOffset = new Vector3 (mainCamera.pixelWidth / 2, bubblePrefab.transform.position.y);
		barrierOffset = new Vector3(mainCamera.pixelWidth / 2, barrierPrefab.transform.position.y);
		cloudOffset = new Vector3 (mainCamera.pixelWidth / 2, cloudPrefab.transform.position.y);
		greyCloudOffset = new Vector3 (mainCamera.pixelWidth / 2, greyCloudPrefab.transform.position.y);
		lastPosXOfBarrier = -1;
		lastPosXOfBubble = -1;
		lastPosXOfCloud = -1;
		lastPosXOfGreyCloud = -1;
		lastHeightOfBubble = -1;
	}
	
	// Update is called once per frame
	void Update () {
		// Barrier generation
		if (transform.position.x - lastPosXOfBarrier >= barriersGap ||
			lastPosXOfBarrier == -1) {
			CreateBarrier ();
		}
		// Bubble generation
		if (transform.position.x - lastPosXOfBubble >= bubblesGap ||
			lastPosXOfBubble == -1) {
			CreateBubble ();
		}
		// Cloud generation
		if (transform.position.x - lastPosXOfCloud >= cloudsGap ||
			lastPosXOfCloud == -1) {
			CreateCloud ();
		}
		// Grey cloud generation
		if (transform.position.x - lastPosXOfGreyCloud >= greyCloudsGap ||
			lastPosXOfGreyCloud == -1) {
			CreateGreyCloud ();
		}
	}
}
