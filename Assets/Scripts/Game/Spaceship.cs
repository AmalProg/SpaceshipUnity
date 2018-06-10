using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;
	
public abstract class Spaceship : MonoBehaviour, IDamageable, IHealable, IMoveable {

	public float _speed;
	public float _rotationSpeed;
	public int _life;
	public int _maxLife;
	public int _hitDamage;
	public float _fireDelay;
	protected float _fireDelayTimer;
	public GameObject lifeUIPrefab;
	protected LifeUi _lifeUI;



	protected void Start() {
		GameObject lifeUIObj = Instantiate(lifeUIPrefab, GameController.lifeUICanvas.transform);
		_lifeUI = lifeUIObj.GetComponent<LifeUi>();
		_lifeUI.SetParent(this.gameObject);

		_hitDamage = 20;
	}

	public virtual void Damage(int d, GameObject caster) {
		
		_life -= d;

		if(_lifeUI)
			_lifeUI.LifeChanged(_life, _maxLife);

		if(_life <= 0) {
			_life = 0;

			Explode(caster);
		}
	}

	public virtual void Heal(int h) { 
		_life += h; 

		if (_life > _maxLife)
			_life = _maxLife;
	} 

	public abstract void Fire ();
	public abstract void Explode (GameObject caster);
	public abstract void Move ();

	void OnDestroy() {
		if(_lifeUI)
			Destroy (_lifeUI.gameObject);
	}
}