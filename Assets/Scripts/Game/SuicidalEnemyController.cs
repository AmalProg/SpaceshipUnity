using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidalEnemyController : SpaceshipEnemy {

	private AI _ai;

	new void Start() {
		base.Start ();

		_ai = new SuicidalAI();
		_life = 20;
		_speed = 3.0f;
		_pointsValue = 400;
		_name = "Suicidal enemy";
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