using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class LaserController : Weapon {

	public float _chargingTime = 1.0f;
	public float _lifeTime = 1.0f;
	public Vector3 _direction;
	private bool _casted = false;

	new public void Start() {
		base.Start ();

		Destroy(this.gameObject, _lifeTime + _chargingTime);

		_damage = 10;
	}

	public void Upload() {
		_chargingTime -= Time.deltaTime;

		if(_chargingTime <= 0.0f && !_casted) {
			int layerMask = 1 << 9 | 1 << 10;
			RaycastHit[] hits = Physics.RaycastAll(_user.transform.position, _direction, 50.0f, layerMask);

			for (int i = 0; i < hits.Length; i++) {
				hits[i].transform.GetComponent<IDamageable>().Damage(_damage, _user);
			}

			_casted = true;
		}
	}
}