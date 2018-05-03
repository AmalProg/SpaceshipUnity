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
		GameObject missile = Instantiate (missilePrefab, transform.position, transform.rotation);
		missile.layer = 8;
		missile.GetComponent<MissileController>().SetUser(this.gameObject);
	}

	new public void Explode (GameObject from) {
		base.Explode (from);
	}

	override public void Move () {
		_ai.Move (this);
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
