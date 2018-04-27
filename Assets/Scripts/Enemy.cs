using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class Enemy : MonoBehaviour {
	private string _name;
}

public class Asteroid : Enemy, IDamageable, IMoveable {

	public float _speed;
	public int _life;

	public void Damage(int d) {
		_life -= d;

		if(_life < 0) {
			_life = 0;

			this.Destroy();
		}
	}

	public void Destroy() {
		Destroy(this);
	}

	public void Move() {
		transform.Translate(0, _speed * Time.deltaTime, 0);
	}
}