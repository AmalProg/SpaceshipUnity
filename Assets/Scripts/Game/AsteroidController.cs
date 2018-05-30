using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class AsteroidController : Enemy {

	public float _speed;
	private int _maxLife;
	public int _size;
	private int _nbrChild;
	private Vector3 _direction;

	new void Start() {
		base.Start ();
		_name = "Asteroid";
		_maxLife = _size * 10;
		_life = _maxLife;
		_pointsValue = _size * 100;
		_nbrChild = Random.Range (1, 7 - _size);
		_direction = new Vector3(transform.up.x, transform.up.y, transform.up.z);
		_direction.Normalize ();
	}

	void Update() {
		Move ();

		float elaspedTime = Time.deltaTime;
		transform.Rotate (20 * elaspedTime, 35 * elaspedTime, 10 * elaspedTime);
	}

	override public void Explode(GameObject caster) {
		if (_size != 1) {
			for (uint i = 0; i < _nbrChild; i++) {
				Quaternion rotation = Random.rotationUniform;
				rotation.eulerAngles = new Vector3 (0, rotation.eulerAngles.y, 90);

				AsteroidController.Spawn (this.gameObject, transform.position, rotation, _size - 1);
			}
		}

		caster.SendMessage ("AddPoints", _pointsValue);
		Destroy(this.gameObject);
	}

	override public void Move() {
		float elaspedTime = Time.deltaTime;
		transform.Translate(_direction * _speed * elaspedTime, Space.World);
	}

	static public GameObject Spawn(GameObject asteroidParent, Vector3 position, Quaternion rotation, int size,
		float speed = -1, int life = -1, int pointsValue = -1) {
		GameObject asteroid = Instantiate (asteroidParent, position, rotation);
		AsteroidController astCtrl = asteroid.GetComponent<AsteroidController> ();
		astCtrl._size = size;
		Vector3 scale = asteroidParent.transform.localScale;
		asteroid.transform.localScale = new Vector3 (scale.x * size / 3, scale.y * size / 3, scale.z * size / 3);
	
		return asteroid;
	}
}