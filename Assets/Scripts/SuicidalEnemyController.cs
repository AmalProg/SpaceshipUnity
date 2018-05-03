using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicidalEnemyController : Enemy {

	public float _speed;
	public GameObject player;

	new void Start() {
		base.Start ();

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
		float elapsedTime = Time.deltaTime;

		float angle = Vector3.Angle(transform.forward, player.transform.position - transform.position);
		transform.Rotate(0, angle * elapsedTime, 0);

		transform.Translate(0, 0, elapsedTime * _speed);
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