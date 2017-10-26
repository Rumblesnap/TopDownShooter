using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

	public GameObject gameManager = null;
	public float speed = 2.0f;
	public GameObject[] weaponsPlacements;
	public GameObject laserPrefab = null;
	public float laserDelay = 1.0f;
	public float timer = 0.0f;
	bool direction = true;
	public float timer2 = 0.0f;
	public float directionDelay = 3.0f;
	public int collision = 0;

	// Use this for initialization
	void Start () {
		direction = true;
		gameManager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void Update () {
		if (direction == true) {
			//move diagonally down/left
			transform.Translate (Vector3.down * Time.deltaTime * speed);
			transform.Translate (Vector3.left * Time.deltaTime * speed);
			}
		if (direction == false) {
			//move diagonally down/right
			transform.Translate (Vector3.down * Time.deltaTime * speed);
			transform.Translate (Vector3.right * Time.deltaTime * speed);
			}

		//changes direction every 3 seconds
		timer2 += Time.deltaTime;
		if (timer2 >= directionDelay) {
			if (direction == true)
				direction = false;
			else
				direction = true;
			timer2 = 0.0f;
		}

		//repeatedly fire enemy's lasers
		timer += Time.deltaTime;
		if (timer >= laserDelay) {
			foreach (GameObject weaponPlacement in weaponsPlacements) {
				Instantiate (laserPrefab, weaponPlacement.transform.position, weaponPlacement.transform.rotation);
			}
			timer = 0.0f;
		}
	}

	void OnCollisionEnter (Collision other) {
		//+1 score if hit by player's laser, destroys enemy object
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

	void OnTriggerEnter (Collider other) {
		//+1 score if hit by player's explosion, destroys enemy object
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
