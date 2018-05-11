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

	static public GameObject Spawn(GameObject suicidalParent, Vector3 position, Quaternion rotation,
		float speed = -1, int life = -1, int pointsValue = -1) {
		GameObject weak = Instantiate (suicidalParent, position, rotation);
		SuicidalEnemyController ctrl = weak.GetComponent<SuicidalEnemyController> ();
		if(speed != -1)
			ctrl._speed = speed;
		if(life != -1)
			ctrl._life = life;
		if(pointsValue != -1)
			ctrl._pointsValue = pointsValue;	

		return weak;
	}
}