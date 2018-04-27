using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : Weapon {

	public void Start() {
		_speed = 6.0f;
		transform.Rotate (0, 90, 90);
	}

	public void SetUser(GameObject user) {
		_user = user;
	}

	override public void Move() {
		transform.Translate(0, _speed * Time.deltaTime, 0);
	}

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer != 10) { //"Map"

			other.gameObject.SendMessage ("Damage", _damage);

			Destroy (this.gameObject);
		}
	}

	public void OnTriggerExit(Collider other) {
		Destroy(this.gameObject);
	}
}