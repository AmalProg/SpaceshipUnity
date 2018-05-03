using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidalEnemyController : Enemy {

	private AI _ai;
	public float _speed;

	void Start() {
		base.Start ();

		_ai = SuicialAI();
		_life = 20;
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

	override public void Move() {
		_ai.Move(this.gameObject);
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