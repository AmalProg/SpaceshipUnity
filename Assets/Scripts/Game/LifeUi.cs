using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class LifeUi : MonoBehaviour
{
	public GameObject _parent;
	private float _lifePercentage;

	void Start() {
		_lifePercentage = 1;
	}

	void Update() {
		Vector3 pos = _parent.transform.position;
		pos.z += 1;
		transform.position = pos;
	}

	public void LifeChanged(float life, float maxLife) {
		_lifePercentage = life / maxLife;

		transform.localScale = new Vector2(_lifePercentage, 1);
	}

	public void SetParent(GameObject parent) {
		_parent = parent;
	}
}
