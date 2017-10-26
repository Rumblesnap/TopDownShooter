using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab = null;
	public GameObject enemy2Prefab = null;
	public GameObject[] enemySpawnPoints;
	public float spawnDelay = 1.0f;
	public float timer = 0.0f;
	public bool enemy2turn;

	// Use this for initialization
	void Start () {
		enemy2turn = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= spawnDelay) {
			int randomNumber = Random.Range (0, enemySpawnPoints.Length);
			Instantiate (enemyPrefab, enemySpawnPoints [randomNumber].transform.position, transform.rotation);
			timer = 0.0f;
			if (enemy2turn == true) {
				randomNumber = Random.Range (0, enemySpawnPoints.Length);
				Instantiate (enemy2Prefab, enemySpawnPoints [randomNumber].transform.position, transform.rotation);
				enemy2turn = false;
			} else {
				enemy2turn = true;
			}
		} 
			
	}
}

