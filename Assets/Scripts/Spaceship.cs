using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;
	
public abstract class Spaceship : MonoBehaviour, IDamageable, IHealable, IMoveable {

	public int _life;
	public int _maxLife;
	public float _fireDelay;
	protected float _fireDelayTimer;

	public void Damage(int d) {
		_life -= d;

		if(_life < 0) {
			_life = 0;

			this.Destroy();
		}
	}

	public void Heal(int h) { 
		_life += h;

		if (_life > _maxLife)
			_life = _maxLife;
	} 

	public abstract void Fire ();
	public abstract void Destroy ();
	public abstract void Move ();
}