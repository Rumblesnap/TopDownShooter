using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject gameManager = null;
	public float speed = 2.0f;
	public int collision = 0;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * Time.deltaTime * speed);
	}

	//+1 score if hit by player's laser, destroys enemy object
	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "PlayerLaser") {
			collision++;
			if (collision == 1)
			{
				gameManager.GetComponent<GameManager> ().score++;
				gameManager.GetComponent<GameManager> ().setText ();
				Destroy(gameObject);
			}
		}
	}

	//+1 score if hit by player's laser, destroys enemy object
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.name == "Explosion") {
			collision++;
			if (collision == 1) {
				gameManager.GetComponent<GameManager> ().score++;
				gameManager.GetComponent<GameManager> ().setText ();
				Destroy (gameObject);
			}
		}
	}
}
