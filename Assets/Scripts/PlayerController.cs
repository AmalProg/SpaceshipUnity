using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Spaceship {

	public float _speed; 
	public float _rotationSpeed;
	private int _points;
	public GameObject missilePrefab;
	 
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
	}

	override public void Fire () {
		float angle = Vector3.Angle(new Vector3(1, 0, 0), Input.mousePosition);
		Quaternion rotation = new Quaternion();
		rotation.Rotate(0, angle, 0);
		GameObject missile = Instantiate (missilePrefab, transform.position, rotation);
		missile.layer = 9;
		missile.GetComponent<MissileController>().SetUser(this.gameObject);
	}
	 
	override public void Explode (GameObject from) {
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
