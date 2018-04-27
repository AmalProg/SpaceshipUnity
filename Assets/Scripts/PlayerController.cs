using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Spaceship {

	public float speed; 
	public float rotationSpeed;
	public GameObject missilePrefab;
	 
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
		GameObject missile = Instantiate (missilePrefab, transform.position, transform.rotation);
		missile.layer = 9;
		missile.GetComponent<MissileController>().SetUser(this.gameObject);
	}
	 
	override public void Destroy () {
		Destroy (this.gameObject);
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
