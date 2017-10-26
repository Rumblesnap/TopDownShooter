using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int score;
	public int lives;
	public Text scoreText;
	public Text livesText;

	// Use this for initialization
	void Start () {
		score = 0;
		lives = 3;
		setText ();
	}
	
	// Update is called once per frame
	void Update () {
		//Pretty self-explanatory
		if (lives == 0)
			GameOver ();	
	}

	// Reset game scene from start
	void GameOver () {
		print ("GAME OVER");
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	// Update values for game score and remaining lives
	public void setText () {
		scoreText.text = "SCORE: " + score.ToString ();
		livesText.text = "LIVES: " + lives.ToString ();
	}

}
