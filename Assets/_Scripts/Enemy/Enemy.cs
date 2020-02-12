public interface IEnemy
{
	int Health { get; set; }
	void KillSelf();
	void DestroyEffect();
}