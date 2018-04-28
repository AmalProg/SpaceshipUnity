using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public abstract class Weapon : MonoBehaviour, IMoveable {
	 
	public float _speed;
	public int _damage;
	protected GameObject _user;

	public void Update() {
		Move(); 
	}

	public abstract void Move();
}