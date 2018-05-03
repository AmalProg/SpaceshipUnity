using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyController : SpaceshipEnemy {

	public float _speed; 
	public float _rotationSpeed;
	public float _turnTime;
	private float _turnTimer = 0; 
	private int moveChoice;
	public GameObject missilePrefab;

	// Use this for initialization
	new void Start () {
		base.Start ();

		_maxLife = 70;
		_life = _maxLife;
		_speed = 6.0f;
		_rotationSpeed = 180;
		_fireDelay = 0.4f;
		_fireDelayTimer = _fireDelay;
		_pointsValue = 1000;
		_name = "Medium enemy";
	}
	
	// Update is called once per frame
	void Update () {
		_fireDelayTimer += Time.deltaTime;

		Move ();

		if (_fireDelayTimer >= _fireDelay) {
			Fire ();

			_fireDelayTimer = 0;
		}
	}

	override public void Fire () {
		GameObject missile = Instantiate (missilePrefab, transform.position, transform.rotation);
		missile.layer = 8;
		missile.GetComponent<MissileController>().SetUser(this.gameObject);
	}

	new public void Explode (GameObject from) {
		base.Explode (from);
	}

	override public void Move () {
		float elapsedTime = Time.deltaTime;
		_turnTimer -= elapsedTime;

		if(_turnTimer <= 0)
			moveChoice = Random.Range (0, 101);

		if (moveChoice < 37) {
			transform.Rotate (new Vector3 (0, _rotationSpeed * elapsedTime, 0));
			_turnTimer = _turnTime;
		} else if (moveChoice < 76) {
			transform.Rotate (new Vector3 (0, -_rotationSpeed * elapsedTime, 0));
			_turnTimer = _turnTime;
		} else {
			_turnTimer = _turnTime;
		}

		transform.Translate(0, 0, elapsedTime * _speed);
	}
}
