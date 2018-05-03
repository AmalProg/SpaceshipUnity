using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyController : SpaceshipEnemy {

	public GameObject missilePrefab;
	private AI _ai;

	// Use this for initialization
	new void Start () {
		base.Start ();

		_ai = new MediumAI();

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
		_ai.Fire(this, missilePrefab);
	}

	new public void Explode (GameObject from) {
		base.Explode (from);
	}

	override public void Move () {
		_ai.Move(this);
	}
}
