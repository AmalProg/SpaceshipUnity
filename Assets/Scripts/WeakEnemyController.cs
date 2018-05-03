using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyController : SpaceshipEnemy {

	public float _speed; 
	public float _rotationSpeed;
	public GameObject missilePrefab;

	// Use this for initialization
	new void Start () {
		base.Start ();

		_maxLife = 50;
		_life = _maxLife;
		_rotationSpeed = 60;
		_turnTime = 1.0f;
		_speed = 4.0f;
		_fireDelay = 0.5f;
		_fireDelayTimer = _fireDelay;
		_pointsValue = 500;
		_name = "Weak enemy";
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

		if (_turnTimer <= 0) {
			moveChoice = Random.Range (0, 101);
			_turnTimer = _turnTime;
		}

		if (moveChoice < 37) {
			transform.Rotate (new Vector3 (_rotationSpeed * elapsedTime, 0, 0));
		} else if (moveChoice < 76) {
			transform.Rotate (new Vector3 (-_rotationSpeed * elapsedTime, 0, 0));
		}

		transform.Translate(0, 0, elapsedTime * _speed);
	}
  
	static public GameObject Spawn(GameObject weakParent, Vector3 position, Quaternion rotation,
		float speed = -1, int life = -1, int pointsValue = -1) {
		GameObject weak = Instantiate (weakParent, position, rotation);
		WeakEnemyController ctrl = weak.GetComponent<WeakEnemyController> ();
		if(speed != -1)
			ctrl._speed = speed;
		if(life != -1)
			ctrl._life = life;
		if(pointsValue != -1)
			ctrl._pointsValue = pointsValue;	

		return weak;
	}
}
