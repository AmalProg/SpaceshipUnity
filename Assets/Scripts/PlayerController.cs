using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Spaceship {

	public float speed;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move ();

		if (Input.GetButtonDown ("Fire1"))
			Fire ();
	}

	override public void Fire () {
		Weapon weapon = new Missile();
		weapon.Use(this.gameObject, new Vector2(0, 0.5f));
		_weapons.Add(weapon);
	}

	override public void Destroy () {
		Destroy (this);
	}

	override public void Move () {
		float elapsedTime = Time.deltaTime;

		float propulsion = Input.GetAxis ("Vertical");

		if (propulsion < 0)
			propulsion = 0;

		transform.Translate(0, 0, propulsion * elapsedTime * speed);
		transform.Rotate(0, Input.GetAxis ("Horizontal") * elapsedTime * rotationSpeed, 0);
	}
}
