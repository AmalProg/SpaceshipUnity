using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;
	
public abstract class Spaceship : MonoBehaviour, IDamageable, IHealable, IMoveable {

	public int _life;
	public int _maxLife;
	public float _fireDelay;
	protected float _fireDelayTimer;

	public void Damage(int d, GameObject from) {
		_life -= d;

		if(_life < 0) {
			_life = 0;

			Explode(from);
		}
	}

	public void Heal(int h) { 
		_life += h; 

		if (_life > _maxLife)
			_life = _maxLife;
	} 

	public abstract void Fire ();
	public abstract void Explode (GameObject from);
	public abstract void Move ();
}