using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class AsteroidController : SpaceshipEnemy {

	public int _size;
	private int _nbrChild;
	private Vector3 _direction;
	private Vector3 _rotation;
	private float _burstSpeed;
	private float _initialBurstSpeed;
	private bool _speedBurst;
	private float _speedBurstTime;
	private float _speedBurstTimer;

	new void Awake() {
		base.Awake ();

		_name = "Asteroid";
		_maxLife = _size * 10;
		_life = _maxLife;
		_pointsValue = _size * 100;
		_nbrChild = Random.Range (1, 7 - _size);
		_burstSpeed = _speed * 2f;
		_speedBurst = false;
		_speedBurstTime = 2f;
		_speedBurstTimer = 0.0f;
	}

	new void Start() {
		base.Start ();

		Vector2 randDir = Random.insideUnitCircle;
		_direction = new Vector3(randDir.x, transform.up.y, randDir.y);
		_direction.Normalize ();

		_rotation = new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));

		Vector3 scale = transform.localScale;
		transform.localScale = new Vector3 (scale.x * _size / 3, scale.y * _size / 3, scale.z * _size / 3);
	}

	void Update() {
		float elaspedTime = Time.deltaTime;

		_speedBurstTimer += elaspedTime;
		if (_speedBurst) {
			_burstSpeed = 3f;
			_initialBurstSpeed = _burstSpeed;

			if (_speedBurstTimer > _speedBurstTime) {
				_burstSpeed = 1f;
				_speedBurst = false;
				_speedBurstTimer = 0.0f;
			}
			else if (_speedBurstTimer > _speedBurstTime / 2f) {
				_burstSpeed = (1f - _speedBurstTimer / _speedBurstTime) * 2f * _initialBurstSpeed;
			}
		}

		Move ();

		transform.Rotate (_rotation.x * elaspedTime, _rotation.y * elaspedTime, _rotation.z * elaspedTime);
	}

	override public void Explode(GameObject caster) {
		if (_size != 1) {
			for (uint i = 0; i < _nbrChild; i++) {
				Quaternion rotation = new Quaternion();
				rotation.eulerAngles = new Vector3 (0, 0, 90);

				AsteroidController.Spawn (this.gameObject, transform.position, rotation, _size - 1);
			}
		}
			
		Destroy(this.gameObject);
		caster.SendMessage ("AddPoints", _pointsValue);
	}

	override public void Move() {
		float elaspedTime = Time.deltaTime;
		transform.Translate(_direction * _speed * _burstSpeed * elaspedTime, Space.World);
	}

	static public GameObject Spawn(GameObject asteroidParent, Vector3 position, Quaternion rotation, int size,
		float speed = -1, int life = -1, int pointsValue = -1) {
		GameObject asteroid = Instantiate (asteroidParent, position, rotation);
		AsteroidController astCtrl = asteroid.GetComponent<AsteroidController> ();
		astCtrl._size = size;
		astCtrl._speedBurst = true;

		return asteroid;
	}

	override public void Fire() {
	}
}