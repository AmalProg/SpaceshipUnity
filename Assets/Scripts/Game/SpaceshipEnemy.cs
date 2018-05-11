using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public abstract class SpaceshipEnemy : Spaceship {

	protected string _name;
	protected int _pointsValue;

	protected void Start() { 
		gameObject.layer = 8;
	}

	override public void Explode (GameObject caster) {
		caster.SendMessage ("AddPoints", _pointsValue);
		Destroy(this.gameObject);
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
}
