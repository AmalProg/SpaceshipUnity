using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class AsteroidController : Enemy {

	public float _speed;
	private int _maxLife;
	public int _size;
	private int _nbrChild;

	new void Start() {
		base.Start ();
		_name = "Asteroid";
		_maxLife = _size * 10;
		_life = _maxLife;
		_pointsValue = _size * 100;
		_nbrChild = Random.Range (1, 7 - _size);
	}

	void Update() {
		Move ();
	}

	override public void Explode(GameObject caster) {
		if (_size != 1) {
			for (uint i = 0; i < _nbrChild; i++) {
				Quaternion rotation = Random.rotationUniform;
				print (rotation.eulerAngles.y);
				float angle = (rotation.eulerAngles.y - 180 / 2) / 2.5f;
				rotation.eulerAngles = new Vector3 (0, transform.eulerAngles.y + angle, 90);

				AsteroidController.Spawn (this.gameObject, _size - 1, transform.position, rotation);
			}
		}

		caster.SendMessage ("AddPoints", _pointsValue);
		Destroy(this.gameObject);
	}

	override public void Move() {
		transform.Translate(0, _speed * Time.deltaTime, 0);
	}

	static public GameObject Spawn(GameObject asteroidParent, int size, Vector3 position, Quaternion rotation,
		float speed = -1, int life = -1, int pointsValue = -1) {
		GameObject asteroid = Instantiate (asteroidParent, position, rotation);
		AsteroidController astCtrl = asteroid.GetComponent<AsteroidController> ();
		astCtrl._size = size;
		Vector3 scale = asteroidParent.transform.localScale;
		asteroid.transform.localScale = new Vector3 (scale.x * size / 3, scale.y * size / 3, scale.z * size / 3);
	
		return asteroid;
	}
}