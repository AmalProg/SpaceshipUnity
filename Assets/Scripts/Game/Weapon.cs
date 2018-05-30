using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

	public int _damage;
	protected GameObject _user;
	protected Vector3 _direction;

	public Vector3 direction { get { return _direction; } set { _direction = value; _direction.Normalize (); } }

	protected void Start() {
		gameObject.tag = "Weapon";
	}

	public void SetUser(GameObject user) {
		_user = user;
	}
}