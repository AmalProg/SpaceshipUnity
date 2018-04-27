using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	public float scrollSpeed; 

	private MeshRenderer _meshRenderer;

	public GameObject player;

	// Use this for initialization
	void Start () {
		_meshRenderer = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	
		scrollTexture ();
	}

	private void scrollTexture() {
		Vector2 offset = new Vector2(-transform.position.x * scrollSpeed, -transform.position.z * scrollSpeed);
		_meshRenderer.material.SetTextureOffset ("_MainTex", offset);
	}
}
