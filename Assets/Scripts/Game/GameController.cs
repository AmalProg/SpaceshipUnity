using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject game;
	public GameObject background;
	public GameObject asteroidPrefab;
	public GameObject weakEnemyPrefab;
	public GameObject suicidalEnemyPrefab;
	public GameObject mediumEnemyPrefab;
	public GameObject playerObj;
	public GameObject gameUIObj;

	private PlayerController player;
	private GameUI gameUI;

	public float _spawnDelay;
	private float _spawnDelayTimer;

	private Dictionary<string, float> _enemySpawnProba = new Dictionary<string, float>(4);
	private Dictionary<string, GameObject> _enemyNameConverter = new Dictionary<string, GameObject>(4);

	// Use this for initialization
	void Start () {
		_spawnDelay = 2.0f;
		_spawnDelayTimer = _spawnDelay;

		player = playerObj.GetComponent<PlayerController> ();
		gameUI = gameUIObj.GetComponent<GameUI> ();

		AI.player = playerObj;

		gameUI.UpdateLife (player.life);
		gameUI.UpdatePoints (player.points);

		_enemyNameConverter ["asteroid"] = asteroidPrefab;
		_enemyNameConverter ["weak"] = weakEnemyPrefab;
		_enemyNameConverter ["suicidal"] = suicidalEnemyPrefab; 
		_enemyNameConverter ["medium"] = mediumEnemyPrefab; 

		_enemySpawnProba ["asteroid"] = 30;
		_enemySpawnProba ["weak"] = 30;
		_enemySpawnProba ["suicidal"] = 30; 
		_enemySpawnProba ["medium"] = 10; 
	}
	
	// Update is called once per frame
	void Update () {
		_spawnDelayTimer += Time.deltaTime;

		if (_spawnDelayTimer > _spawnDelay) {
			SpawnEnemy ();

			_spawnDelayTimer = 0.0f;
		}

		if (player.isDead)
			endGame ();
		if(player.hasWonPoints)
			gameUI.UpdatePoints (player.points);
		if(player.hasLifeChanged)
			gameUI.UpdateLife (player.life);
		
	}

	public void endGame() {
		GlobalData.finalScore = player.points;

		SceneLoader.LoadScene ("EndMenuScene");
	}

	private void SpawnEnemy() {
		int enemyChoiceProb = Random.Range (0, 101);
		float actualProbMax = 0;
		foreach (KeyValuePair<string, float> kvp in _enemySpawnProba) {
			actualProbMax += kvp.Value;
			if (actualProbMax >= enemyChoiceProb) {
				GenerateEnemy(kvp.Key);
				break;
			}
		}
	}

	private void GenerateEnemy(string name) {
		Collider backgroundCollider = background.GetComponent<Collider> ();
		Vector3 max = backgroundCollider.bounds.max;
		Vector3 min = backgroundCollider.bounds.min;
		Vector3 spawnPosition = new Vector3();
		int side = Random.Range (0, 4);
		switch (side) { // choisit le coté du spawn
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

		Quaternion spawnRotation = Random.rotation;
		spawnRotation.eulerAngles = new Vector3 (0, spawnRotation.eulerAngles.y, 90);

		switch (name) {
		case "asteroid":
			AsteroidController.Spawn (_enemyNameConverter [name], spawnPosition, spawnRotation, 3);
			break;
		case "weak":
			WeakEnemyController.Spawn (_enemyNameConverter [name], spawnPosition, spawnRotation);
			break;
		case "suicidal":
			SuicidalEnemyController.Spawn (_enemyNameConverter [name], spawnPosition, spawnRotation);
			break;
		case "medium":
			MediumEnemyController.Spawn (_enemyNameConverter [name], spawnPosition, spawnRotation);
			break;
		default:
			break;
		}
	}
}
