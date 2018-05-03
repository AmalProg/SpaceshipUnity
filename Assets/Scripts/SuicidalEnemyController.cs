using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidalEnemyController : Enemy {

	private AI _ai;
	public float _speed;

	void Start() {
		base.Start ();

		_ai = SuicialAI();
		_speed = 3.0f;
		_pointsValue = 400;
		_name = "Suicidal enemy";
	}

	void Update () {
		Move ();
	}

	new public void Explode (GameObject from) {
		base.Explode (from);
	}

	new public void Move() {
		_ai.Move(this.gameObject);
	}
}