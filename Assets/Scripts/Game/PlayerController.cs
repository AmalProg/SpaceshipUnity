using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Spaceship {

	private int _points;
	public GameObject missilePrefab;
	private bool _hasWonPoints;
	private bool _hasLifeChanged;
	private bool _isDead;
	private int _level;

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
	public bool isDead { get { return _isDead; } }

	// Use this for initialization
	new void Start () {
		base.Start();

		_maxLife = 100;
		_life = _maxLife;
		_speed = 8.0f;
		_rotationSpeed = 180;
		_fireDelayTimer = 0.0f;
		_fireDelay = 0.2f;
		_isDead = false;
		_level = 1;
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

	public void Evolve() {
		_level++;
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
		Vector3 pos = transform.position;
		Vector3 focus = Input.mousePosition;// + transform.position - from; // les différent tir ne vont pas sur le pointeur de la souris
		focus.z = focus.y;
		focus = Camera.main.ScreenToWorldPoint (focus);
		focus.y = transform.position.y;
		Fire (focus, pos);

		if (_level >= 2) {
			Vector3 normalFocus = Vector3.Cross (focus - pos, Vector3.up).normalized;

			Vector3 focus2 = Vector3.Slerp (focus, normalFocus, 0.08f);
			Fire (focus2, transform.position + normalFocus * 0.2f);
		}
		if (_level >= 3) {
			Vector3 normalFocus = -Vector3.Cross (focus - pos, Vector3.up).normalized;

			Vector3 focus2 = Vector3.Slerp (focus, normalFocus, 0.08f);
			Fire (focus2, transform.position + normalFocus * 0.2f);
		}
	}

	private void Fire(Vector3 focus, Vector3 from) {
		GameObject missile = Instantiate (missilePrefab, from, new Quaternion());
		missile.layer = 9;
		MissileController missileComponent = missile.GetComponent<MissileController> ();
		missileComponent.direction = focus - from;
		missileComponent.SetUser(this.gameObject);
	}

	override public void Explode (GameObject caster) {
		_isDead = true;
	}

	override public void Move () {
		float elapsedTime = Time.deltaTime;

		float propulsion = Input.GetAxis ("Vertical");

		if (propulsion < 0) // on en peux pas reculer
			propulsion = 0;

		transform.Translate(0, 0, propulsion * elapsedTime * _speed);
		transform.Rotate(0, Input.GetAxis ("Horizontal") * elapsedTime * _rotationSpeed, 0, Space.World);
	}
}
