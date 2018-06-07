using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public abstract class SpaceshipEnemy : Spaceship {

	protected string _name;
	protected int _pointsValue;
	protected Vector3 _fireDirection;

	public Vector3 fireDirection { get { return _fireDirection; } set { _fireDirection = value; } }

	new protected void Start() { 
		base.Start();
		
		gameObject.layer = 8;
	}

	override public void Explode (GameObject caster) {
		Destroy(this.gameObject);
		caster.SendMessage ("AddPoints", _pointsValue);
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

	static public GameObject Spawn(GameObject parent, Vector3 position, Quaternion rotation,
		int speed = -1, int life = -1, int pointsValue = -1) {
		GameObject spac = Instantiate (parent, position, rotation);
		SpaceshipEnemy ctrl = spac.GetComponent<SpaceshipEnemy> ();
		if(speed != -1)
			ctrl._speed = speed / 100.0f;
		if(life != -1)
			ctrl._life = life;
		if(pointsValue != -1)
			ctrl._pointsValue = pointsValue;	

		return spac;
	}
}
