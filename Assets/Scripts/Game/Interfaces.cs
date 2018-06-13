using UnityEngine;

namespace nInterfaces {
	public interface IDamageable {
		void Damage(int d, GameObject caster);
		void Explode(GameObject caster);
	}

	public interface IHealable {
		void Heal(int h);
	}

	public interface IMoveable {
		void Move();
	}
}