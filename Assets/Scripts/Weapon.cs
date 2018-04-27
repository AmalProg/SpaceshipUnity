using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public abstract class Weapon : MonoBehaviour, IMoveable {

	public float _speed;
	public uint _damage;
	protected GameObject _user;

	public void Update() {
		Move();
	}

	public void Use(GameObject user, Vector2 offset) {
		_user = user;
	}

	public abstract void Move();
}

public class Missile : Weapon {

	public void Start() {

	}

	public void Use(GameObject user, Vector2 offset) {
		base.Use(user, offset);

		transform.position = new Vector3 (_user.transform.position.x + offset.x, 
			_user.transform.position.y,
			_user.transform.position.z + offset.y);

		transform.rotation = _user.transform.rotation;
	}

	override public void Move() {
		transform.Translate(0, _speed * Time.deltaTime, 0);
	}

	public void OnTriggerEnter2D(Collision2D other) {
		other.gameObject.SendMessage("Damage", _damage);

		Destroy(this);
	}

	public void OnTriggerExit2D(Collider2D other) {
		Destroy(this);
	}
}