using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidalEnemyController : SpaceshipEnemy {

	private AI _ai;

	new void Awake() {
		base.Awake ();

		_life = 20;
		_maxLife = _life;
		_speed = 7.0f;
		_rotationSpeed = 100f;
		_pointsValue = 400;
		_name = "Suicidal enemy";
	}

	new void Start() {
		base.Start ();

		_ai = new SuicidalAI();
	}
		
	void Update () {
		Move ();
	}

	new public void Explode (GameObject caster) {
		base.Explode (caster);
	}

	override public void Move() {
		_ai.Move(this);
	}

	override public void Fire() {
	}
}