using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI {
	
	static public GameObject player;

	public abstract void Move(SpaceshipEnemy entity);
	public abstract void Fire(SpaceshipEnemy entity, GameObject weapon);
	public abstract void UpdateFireDirection (SpaceshipEnemy entity);
}

public class SuicidalAI : AI {
	override public void Move(SpaceshipEnemy entity) {
		float elapsedTime = Time.deltaTime;
		Vector3 playerDir = player.transform.position - entity.transform.position;
		float angle = Vector3.Angle(entity.transform.forward, playerDir);
		Vector3 cross = Vector3.Cross (entity.transform.forward, playerDir);
		if (cross.y < 0)
			angle *= -1;
		entity.transform.Rotate(angle * elapsedTime, 0, 0);

		entity.transform.Translate(0, 0, elapsedTime * entity._speed);
	}

	override public void Fire(SpaceshipEnemy entity, GameObject weapon) {
	}

	override public void UpdateFireDirection (SpaceshipEnemy entity) {
	}
}

public class WeakAI : AI {

	public float _turnTime = 1.0f;
	private float _turnTimer = 0; 
	private int moveChoice;

	override public void Move(SpaceshipEnemy entity) {
		float elapsedTime = Time.deltaTime;
		_turnTimer -= elapsedTime;

		if(_turnTimer <= 0) {
			moveChoice = Random.Range (0, 101);
			_turnTimer = _turnTime;
		}

		if (moveChoice < 37) {
			entity.transform.Rotate (new Vector3 (0, entity._rotationSpeed * elapsedTime, 0), Space.World);
		} else if (moveChoice < 76) {
			entity.transform.Rotate (new Vector3 (0, -entity._rotationSpeed * elapsedTime , 0), Space.World);
		}

		entity.transform.Translate(0, -elapsedTime * entity._speed, 0);
	}

	override public void Fire(SpaceshipEnemy entity, GameObject weapon) {
		GameObject weaponObj = Object.Instantiate (weapon, entity.transform.position, entity.transform.rotation);
		weaponObj.layer = 8;
		Weapon weaponComponent = weaponObj.GetComponent<Weapon> ();
		weaponComponent.direction = -entity.transform.up;
		weaponComponent.SetUser(entity.gameObject);
	}

	override public void UpdateFireDirection (SpaceshipEnemy entity) {
		entity.fireDirection = -entity.transform.up;
	}
}

public class MediumAI : AI {

	public float _turnTime = 1.0f;
	private float _turnTimer = 0; 
	private int moveChoice;

	override public void Move(SpaceshipEnemy entity) {
		float elapsedTime = Time.deltaTime;
		_turnTimer -= elapsedTime;

		if(_turnTimer <= 0) {
			moveChoice = Random.Range (0, 101);
			_turnTimer = _turnTime;
		}

		if (moveChoice < 37) {
			entity.transform.Rotate (new Vector3 (0, 0, entity._rotationSpeed * elapsedTime));
		} else if (moveChoice < 76) {
			entity.transform.Rotate (new Vector3 (0, 0, -entity._rotationSpeed * elapsedTime));
		}

		entity.transform.Translate(elapsedTime * entity._speed, 0, 0);
	}

	override public void Fire(SpaceshipEnemy entity, GameObject weapon) {
		GameObject weaponObj = Object.Instantiate (weapon, entity.transform.position, entity.transform.rotation);
		weaponObj.layer = 8;
		Weapon weaponComponent = weaponObj.GetComponent<Weapon> ();
		weaponComponent.direction = player.transform.position - entity.transform.position;
		weaponComponent.SetUser(entity.gameObject);
	}

	override public void UpdateFireDirection (SpaceshipEnemy entity) {
		entity.fireDirection = player.transform.position - entity.transform.position;
	}
}