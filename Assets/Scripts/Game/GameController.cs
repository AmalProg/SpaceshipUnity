using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using UnityEngine;

public class GameController : MonoBehaviour {

	static public GameObject lifeUICanvas;

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
	public float _difficultyUpDelay;
	private float _difficultyUpDelayTimer;

	public float _evolveDelay;
	private float _evolveDelayTimer;

	public float _averageStatUpDelay;
	private float _averageStatUpDelayTimer;

	private Dictionary<string, int> _enemySpawnProba = new Dictionary<string, int>(4);
	private Dictionary<string, GameObject> _enemyNameConverter = new Dictionary<string, GameObject>(4);
	private Dictionary<string, Dictionary<string, int>> _enemyStatsAverage = new Dictionary<string, Dictionary<string, int>>(4);
	private Dictionary<string, float> _randomPercentByStat = new Dictionary<string, float>();

	void Awake() {
		lifeUICanvas = GameObject.Find ("LifeUI");
	}

	// Use this for initialization
	void Start () {
		_spawnDelay = 5.0f;
		_spawnDelayTimer = _spawnDelay;

		_difficultyUpDelay = 20.0f;
		_difficultyUpDelayTimer = 0.0f;

		_evolveDelay = 60f;
		_evolveDelayTimer = 0f;

		_averageStatUpDelay = 10f;
		_averageStatUpDelayTimer = 0.0f;

		player = playerObj.GetComponent<PlayerController> ();
		gameUI = gameUIObj.GetComponent<GameUI> ();

		AI.player = playerObj;

		gameUI.UpdateLife (player.life);
		gameUI.UpdatePoints (player.points);			

		_enemyNameConverter ["asteroid"] = asteroidPrefab;
		_enemyNameConverter ["weak"] = weakEnemyPrefab;
		_enemyNameConverter ["suicidal"] = suicidalEnemyPrefab; 
		_enemyNameConverter ["medium"] = mediumEnemyPrefab; 

		XmlDocument doc = new XmlDocument();
		try { // charge les stats de base des enemis via un fichier xml
			doc.Load ("./Assets/Datas/EnemiesStats.xml");
			foreach(XmlNode enemyNode in doc.SelectNodes("/EnemiesStats/Enemy")) {
				string enemyType = enemyNode.Attributes["type"].Value;
				_enemyStatsAverage[enemyType] = new Dictionary<string, int>();
				foreach(XmlNode enemyStat in enemyNode.SelectNodes("Stat")) {
					int statValue = int.Parse(enemyStat.InnerText, CultureInfo.InvariantCulture.NumberFormat);
					_enemyStatsAverage [enemyType] [enemyStat.Attributes["name"].Value] = statValue;
				}
				int probValue = int.Parse(enemyNode.SelectSingleNode("Prob").InnerText, CultureInfo.InvariantCulture.NumberFormat);
				_enemySpawnProba [enemyType] = probValue;
			}
		}
		catch(System.IO.FileNotFoundException) {
			doc.LoadXml("");
		}

		_randomPercentByStat ["size"] = 0.3f;
		_randomPercentByStat ["speed"] = 0.2f;
		_randomPercentByStat ["life"] = 0.6f;
		_randomPercentByStat ["pointValue"] = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		float elaspedTime = Time.deltaTime;

		_spawnDelayTimer += elaspedTime;
		_difficultyUpDelayTimer += elaspedTime;
		_evolveDelayTimer += elaspedTime;
		_averageStatUpDelayTimer += elaspedTime;

		if (_difficultyUpDelayTimer > _difficultyUpDelay && _spawnDelay > 1.0f) {
			_spawnDelay -= 0.5f;

			_difficultyUpDelayTimer = 0.0f;
		}

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

		if (_evolveDelayTimer > _evolveDelay) {
			player.Evolve ();

			_evolveDelayTimer = 0f;
		}

		if (_averageStatUpDelayTimer > _averageStatUpDelay) {
			AverageStatsUp ();

			_averageStatUpDelayTimer = 0f;
		}
	}

	public void endGame() {
		GlobalData.finalScore = player.points;

		SceneLoader.LoadScene ("EndMenuScene");
	}

	private void SpawnEnemy() {
		int randMax = 0;
		foreach (KeyValuePair<string, int> kvp in _enemySpawnProba) {
			randMax += kvp.Value;
		}

		int enemyChoiceProb = Random.Range (0, randMax + 1);
		int actualProbMax = 0;
		foreach (KeyValuePair<string, int> kvp in _enemySpawnProba) {
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

		Dictionary<string, int> stats = new Dictionary<string, int>();
		foreach (KeyValuePair<string, int> kvp in _enemyStatsAverage[name]) {
			stats [kvp.Key] = RandomizeStat (kvp.Value, _randomPercentByStat[kvp.Key]);
		}

		switch (name) {
		case "asteroid":
			spawnRotation.eulerAngles = new Vector3 (0, 0, 90);
			AsteroidController.Spawn (_enemyNameConverter [name], spawnPosition, spawnRotation, stats["size"], stats["speed"]
				, stats["life"], stats["pointValue"]);
			break;
		case "weak":
			spawnRotation.eulerAngles = new Vector3 (0, 0, 90);
			WeakEnemyController.Spawn (_enemyNameConverter [name], spawnPosition, spawnRotation, stats["speed"]
				, stats["life"], stats["pointValue"]);
			break;
		case "suicidal":
			spawnRotation.eulerAngles = new Vector3 (0, 0, 90);
			SuicidalEnemyController.Spawn (_enemyNameConverter [name], spawnPosition, spawnRotation, stats["speed"]
				, stats["life"], stats["pointValue"]);
			break;
		case "medium":
			spawnRotation.eulerAngles = new Vector3 (90, 0, 0);
			MediumEnemyController.Spawn (_enemyNameConverter [name], spawnPosition, spawnRotation, stats["speed"]
				, stats["life"], stats["pointValue"]);
			break;
		default:
			break;
		}
	}

	private int RandomizeStat(int baseValue, float percentage) {
		return (int)Random.Range(baseValue - baseValue * percentage / 2, baseValue + baseValue * percentage / 2);
	}

	private void AverageStatsUp() {
		foreach (string enemyName in _enemyStatsAverage.Keys) {
			List<string> keyStatsNameList = new List<string> (_enemyStatsAverage[enemyName].Keys);
			foreach (string statsName in keyStatsNameList) {
				_enemyStatsAverage[enemyName][statsName] = (int)(_enemyStatsAverage[enemyName][statsName] * 1.04f);
			}
		}
	}
}
