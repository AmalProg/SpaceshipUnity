using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Spaceship {

	private int _points;
	public GameObject missilePrefab;
	public bool _hasWonPoints;
	public bool _hasLifeChanged;

	public int points { get { return _points; } }
	public int life { get { return _life; } }
	public bool hasWonPoints { get { 
			bool tmp = _hasWonPoints;
			_hasWonPoints = false;
			return tmp; } }
	public bool hasLifeChanged { get { 
			bool tmp = _hasLifeChanged;
			_hasLifeChanged = false;
			return tmp; } }

	// Use this for initialization
	void Start () {
		_maxLife = 100;
		_life = _maxLife;
		_speed = 8.0f;
		_rotationSpeed = 180;
		_fireDelayTimer = 0.0f;
		_fireDelay = 0.2f;
	}

	// Update is called once per frame
	void Update () {
		_fireDelayTimer += Time.deltaTime;

		Move ();

		if (Input.GetButton ("Fire1") && _fireDelayTimer >= _fireDelay) {
			Fire ();

			_fireDelayTimer = 0;
		}
	}

	public void AddPoints(int pts) {
		_points += pts;
		_hasWonPoints = true;
	}

	override public void Damage(int d, GameObject caster) {
		base.Damage (d, caster);
		_hasLifeChanged = true;
	}
		

	override public void Fire () {
		Quaternion rotation = new Quaternion();
		Vector3 pos = Input.mousePosition;
		pos.z = pos.y;
		pos = Camera.main.ScreenToWorldPoint (pos);
		pos.y = 0.5f;
		rotation.SetLookRotation(pos);
		GameObject missile = Instantiate (missilePrefab, transform.position, rotation);
		missile.layer = 9;
		missile.GetComponent<MissileController>().SetUser(this.gameObject);
	}

	override public void Explode (GameObject caster) {
		Destroy (this.gameObject);
	}

	override public void Move () {
		float elapsedTime = Time.deltaTime;

		float propulsion = Input.GetAxis ("Vertical");

		if (propulsion < 0)
			propulsion = 0;

		transform.Translate(0, 0, propulsion * elapsedTime * _speed);
		transform.Rotate(0, Input.GetAxis ("Horizontal") * elapsedTime * _rotationSpeed, 0);
	}
}
