using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;
using UnityEngine.UI;

public class LifeUi : MonoBehaviour
{
	public GameObject _parent;
	private float _size;
	private float _lifePercentage;
	private Vector2 _baseScale;
	private Image _image;

	void Start() {
		_lifePercentage = 1;
		_baseScale = new Vector2 (transform.localScale.x, transform.localScale.y);
		_image = GetComponent<Image> ();
		_image.color = Color.green;
		_size = _parent.GetComponent<Renderer> ().bounds.size.z;
	}

	void Update() {
		Vector3 pos = _parent.transform.position;
		pos.z += _size;
		transform.position = pos;
	}

	public void LifeChanged(float life, float maxLife) {
		if (life > 0)
			_lifePercentage = life / maxLife;
		else
			_lifePercentage = 0.0f;

		if (_lifePercentage <= 0.25f)
			_image.color = Color.red;
		else if (_lifePercentage <= 0.5f)
			_image.color = Color.yellow;

		transform.localScale = new Vector2(_baseScale.x, _baseScale.y * _lifePercentage);
	}

	public void SetParent(GameObject parent) {
		_parent = parent;
	}
}
