using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI {
	
	static public GameObject player;

	public abstract void Move(SpaceshipEnemy entity);
	public abstract void Fire(SpaceshipEnemy entity, GameObject weapon);
}

public class SuicidalAI : AI {
	override public void Move(SpaceshipEnemy entity) {
		float elapsedTime = Time.deltaTime;
		float angle = Vector3.Angle(entity.transform.forward, player.transform.position - entity.transform.position);
		entity.transform.Rotate(angle * elapsedTime, 0, 0);

		entity.transform.Translate(0, 0, elapsedTime * entity._speed);
	}

	override public void Fire(SpaceshipEnemy entity, GameObject weapon) {

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
			entity.transform.Rotate (new Vector3 (entity._rotationSpeed * elapsedTime, 0, 0));
		} else if (moveChoice < 76) {
			entity.transform.Rotate (new Vector3 (-entity._rotationSpeed * elapsedTime, 0, 0));
		}

		entity.transform.Translate(0, 0, elapsedTime * entity._speed);
	}

	override public void Fire(SpaceshipEnemy entity, GameObject weapon) {
		GameObject weaponObj = Object.Instantiate (weapon, entity.transform.position, entity.transform.rotation);
		weaponObj.layer = 8;
		weaponObj.GetComponent<Weapon>().SetUser(entity.gameObject);
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
			entity.transform.Rotate (new Vector3 (0, entity._rotationSpeed * elapsedTime, 0));
		} else if (moveChoice < 76) {
			entity.transform.Rotate (new Vector3 (0, -entity._rotationSpeed * elapsedTime, 0));
		}

		entity.transform.Translate(0, 0, elapsedTime * entity._speed);
	}

	override public void Fire(SpaceshipEnemy entity, GameObject weapon) {
		Quaternion rotation = entity.transform.rotation;
		rotation.SetLookRotation(player.transform.position);
		GameObject weaponObj = Object.Instantiate (weapon, entity.transform.position, rotation);
		weaponObj.layer = 8;
		weaponObj.GetComponent<Weapon>().SetUser(entity.gameObject);
	}
}