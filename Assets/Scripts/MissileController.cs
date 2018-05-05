using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class MissileController : Weapon, IMoveable {

	public float _speed;

	new public void Start() {
		base.Start ();

		_speed = 15.0f;
		_damage = 10;
	}

	public void Update() {
		Move(); 
	}

	public void Move() {
		transform.Translate(0, 0, _speed * Time.deltaTime);
	}

	public void OnTriggerEnter(Collider other) {
		if(other != null) {
			if (other.gameObject.layer != 10 && !other.CompareTag("Weapon")) { //"Map"

				other.gameObject.GetComponent<IDamageable>().Damage(_damage, _user);

				Destroy (this.gameObject);
			}
		}
	}

	public void OnTriggerExit(Collider other) {
		Destroy(this.gameObject);
	}
}