using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class AsteroidController : Enemy, IDamageable, IMoveable {

	public float _speed;
	public int _life;
	public int _size;
	private int _nbrChild;

	new void Start() {
		base.Start ();

		_name = "Asteroid";

		_speed = 5.0f;
		_life = 30;

		_nbrChild = Random.Range (1, 7 - _size);
	}

	void Update() {
		Move ();
	}

	public void Damage(int d) {
		_life -= d;

		if(_life < 0) {
			_life = 0;

			this.Destroy();
		}
	}

	public void Destroy() {
		if (_size != 1) {
			for (uint i = 0; i < _nbrChild; i++) {
				Quaternion rotation = Random.rotation;
				rotation.eulerAngles = new Vector3 (rotation.eulerAngles.x, rotation.eulerAngles.y, 90);

				AsteroidController.Spawn (this.gameObject, _size - 1, transform.position, rotation);
			}
		}

		Destroy(this.gameObject);
	}

	public void Move() {
		transform.Translate(0, _speed * Time.deltaTime, 0);
	}

	static public GameObject Spawn(GameObject asteroidParent, int size, Vector3 position, Quaternion rotation = new Quaternion()) {
		GameObject asteroid = Instantiate (asteroidParent, position, rotation);
		AsteroidController astCtrl = asteroid.GetComponent<AsteroidController> ();
		astCtrl._size = size;
		astCtrl._life = size * 10;
		Vector3 scale = asteroidParent.transform.localScale;
		asteroid.transform.localScale = new Vector3 (scale.x * size / 3, scale.y * size / 3, scale.z * size / 3);
	
		return asteroid;
	}
}