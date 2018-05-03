
public abstract class AI {
	
	public GameObject player;

	public abstract void Move(GameObject  );
	public abstract void Fire(GameObject entity, GameObject weapon);
}

public class SuicidalAI : AI {
	public void Move(GameObject entity) {
		float angle = Vector3.Angle(entity.transform.forward, player.transform.position - entity.transform.position);
		entity.transform.Rotate(0, angle * elapsedTime, 0);

		entity.transform.Translate(0, 0, elapsedTime * entity._speed);
	}

	public void Fire(GameObject weapon) {

	}
}

public class MediumAI : AI {

	public float _turnTime = 1.0f;
	private float _turnTimer = 0; 
	private int moveChoice;

	public void Move(GameObject entity) {
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

	public void Fire(GameObject entity, GameObject weapon) {
		float angle = Vector3.Angle(entity.transform.forward, player.transform.position - entity.transform.position);
		Vector3 rotation = entity.transform.rotation;
		rotation.y += angle;
		GameObject weaponObj = Instantiate (weapon, entity.transform.position, rotation);
		weaponObj.layer = 8;
		weaponObj.GetComponent<Weapon>().SetUser(entity);
	}
}