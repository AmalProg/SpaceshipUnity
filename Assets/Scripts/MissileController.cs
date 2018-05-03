using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class MissileController : Weapon {

	public void Start() {
		_speed = 15.0f;
		_damage = 10;
	}

	override public void Move() {
		transform.Translate(0, 0, _speed * Time.deltaTime);
	}

	public void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer != 10) { //"Map"

			other.gameObject.GetComponent<IDamageable>().Damage(_damage, this.gameObject);

			Destroy (this.gameObject);
		}
	}

	public void OnTriggerExit(Collider other) {
		Destroy(this.gameObject);
	}
}