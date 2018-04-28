using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject background;
	public GameObject asteroidPrefab;
	public float _spawnDelay;
	private float _spawnDelayTimer;

	// Use this for initialization
	void Start () {
		_spawnDelay = 2.0f;
		_spawnDelayTimer = _spawnDelay;
	}
	
	// Update is called once per frame
	void Update () {
		_spawnDelayTimer += Time.deltaTime;

		if (_spawnDelayTimer > _spawnDelay) {
			spawnEnemy ();

			_spawnDelayTimer = 0.0f;
		}
	}

	private void spawnEnemy() {
		Collider backgroundCollider = background.GetComponent<Collider> ();
		Vector3 max = backgroundCollider.bounds.max;
		Vector3 min = backgroundCollider.bounds.min;
		Vector3 spawnPosition = new Vector3();
		int side = Random.Range (0, 5);
		switch (side) {
		case 0:
			spawnPosition.x = max.x;
			spawnPosition.y = 0.5f;
			spawnPosition.z = Random.Range (min.z, max.z);
			break;
		case 1:
			spawnPosition.x = min.x;
			spawnPosition.y = 0.5f;
			spawnPosition.z = Random.Range (min.z, max.z);
			break;
		case 2:
			spawnPosition.x = Random.Range (min.x, max.x);
			spawnPosition.y = 0.5f;
			spawnPosition.z = max.z;
			break;
		case 3:
			spawnPosition.x = Random.Range (min.x, max.x);
			spawnPosition.y = 0.5f;
			spawnPosition.z = min.z;
			break;
		}
		Quaternion rotation = Random.rotation;
		rotation.eulerAngles = new Vector3 (rotation.eulerAngles.x, rotation.eulerAngles.y, 90);
		AsteroidController.Spawn(asteroidPrefab, 3, spawnPosition, rotation);
	}
}
