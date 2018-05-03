using UnityEngine;

namespace nInterfaces {

public interface IDamageable {
	void Damage(int d, GameObject from);
	void Explode(GameObject from);
}

public interface IHealable {
	void Heal(int h);
}

public interface IMoveable {
	void Move();
}

}