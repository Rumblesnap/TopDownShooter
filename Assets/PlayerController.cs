using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject gameManager = null;

	public Vector3 startPos;

	public int collision = 0;

	//data for controlling explosion attack
	public GameObject explosion = null;
	public bool canExplode;
	public float timer = 0.0f;
	public float explodeDelay = 3.0f;

	//player speed
	public float speed = 10.0f;

	//firin mah lazers
	public GameObject[] weaponsPlacements;
	public GameObject laserPrefab = null;

	//player shield
	public GameObject shield = null;
	public bool isUsingShield = false;

	// Use this for initialization
	void Start () {
		shield.SetActive (false);
		explosion.SetActive (false);
		canExplode = true;
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		//increment timer
		timer += Time.deltaTime;

		//directional controls
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (Vector3.down * Time.deltaTime * speed);
		}

		//fire lasers for all weapon placements, if shield not up
		if (Input.GetKeyDown (KeyCode.Space) && isUsingShield == false) {
			foreach (GameObject weaponPlacement in weaponsPlacements) {
				Instantiate (laserPrefab, weaponPlacement.transform.position, weaponPlacement.transform.rotation);
			}
		}

		//toggle shield with left shift
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			shield.SetActive (true);
			isUsingShield = true;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			shield.SetActive (false);
			isUsingShield = false;
		}

		//after a 3-second cooldown has passed after use, explosion attack can be used with left control
		if (canExplode == false) {
			if (timer >= explodeDelay) {
				canExplode = true;
			}
		} else if (canExplode == true) {
			if (Input.GetKeyDown (KeyCode.LeftControl) && isUsingShield == false) {
				explode (canExplode);
			}
		}


			
	}
		
	void OnCollisionEnter (Collision other)
	{
		if (isUsingShield != true) {
			if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Laser") {
				if (explosion.activeInHierarchy == true) {
					gameManager.GetComponent<GameManager> ().score++;
					Destroy (other.gameObject);
				}
				else {
					collision++;
					if (collision == 1) {
						gameManager.GetComponent<GameManager> ().lives -= 1;
						gameManager.GetComponent<GameManager> ().setText ();
						transform.position = startPos;
						//take away 1 life and reset position whenever player hits an enemy or an enemy laser
					}
				}
			}
		}
	}

	void OnCollisionExit (Collision other)
	{
		collision = 0;
	}


		

	//sets explosion to active and deactivates after 1 second via subsequent function
	void explode (bool canExplode) {
		if (canExplode == true) {
			explosion.SetActive (true);
			Invoke ("endExplosion", 1);
		} else
			endExplosion ();
	}

	void endExplosion () {
		explosion.SetActive (false);
		canExplode = false;
		timer = 0.0f;
	}

}
