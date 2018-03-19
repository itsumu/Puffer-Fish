using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	int score = 0;

	public static GameController instance;
	public GameObject gameOverLabel;
	public Text scoreText;
	public bool gameOver = false;
	public AudioClip gameOverSound;
	private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (this.gameObject);
			return;
		}
		this.audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver && Input.anyKeyDown) {
			RestartGame ();
		}
	}

	public void GetScore() {
		score++;
		scoreText.text = "Score: " + score.ToString ();
	}

	public void FishDone() {
		gameOverLabel.SetActive (true);
		gameOver = true;
		this.audioSource.PlayOneShot (gameOverSound);
		Time.timeScale = 0;
	}

	void RestartGame() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (
			UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
	}
}
