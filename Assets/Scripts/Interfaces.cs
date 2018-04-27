namespace nInterfaces {

public interface IDamageable {
	void Damage(int d);
	void Destroy();
}

public interface IHealable {
	void Heal(int h);
}

public interface IMoveable {
	void Move();
}

}