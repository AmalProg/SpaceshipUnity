using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemyController : SpaceshipEnemy {

	private AI _ai;
	public GameObject missilePrefab;

	// Use this for initialization
	new void Start () {
		base.Start ();

		_ai = new WeakAI ();
		_maxLife = 50;
		_life = _maxLife;
		_rotationSpeed = 60;
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
		_ai.Fire (this, missilePrefab);
	}

	new public void Explode (GameObject caster) {
		base.Explode (caster);
	}

	override public void Move () {
		_ai.Move (this);
	}
}
