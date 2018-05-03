using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public abstract class Enemy : MonoBehaviour, IDamageable, IMoveable {
	protected string _name;
	protected int _life;
	protected int _pointsValue;

	protected void Start() {
		gameObject.layer = 8; // "Enemy"
	}

	protected void switchMapSide(Collider other) {
		Vector3 colPoint = other.bounds.ClosestPoint (transform.position);
		Vector3 max = other.bounds.max;
		Vector3 min = other.bounds.min;

		if (colPoint.x == max.x) {
			transform.position = new Vector3(min.x, transform.position.y, transform.position.z);
		} else if (colPoint.x == min.x) {
			transform.position = new Vector3(max.x, transform.position.y, transform.position.z);;
		}

		if (colPoint.z == max.z) {
			transform.position = new Vector3(transform.position.x, transform.position.y, min.z);
		} else if (colPoint.z == min.z) {
			transform.position = new Vector3(transform.position.x, transform.position.y, max.z);
		}
	}

	public void OnTriggerExit(Collider other) {
		if (other.gameObject.layer == 10) { //"Map"
			switchMapSide (other);
		}
	}

	public void Damage(int d, GameObject from) {
		_life -= d;

		if(_life < 0) {
			_life = 0;

			this.Explode(from);
		}
	}

	public void Explode(GameObject from) {
		from.SendMessage ("AddPoints", _pointsValue);
		Destroy(this.gameObject);
	}
  
	public abstract void Move();
}