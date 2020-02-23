using UnityEngine;
public interface IEnemy
{
	void Initialize(Vector3 pos, Transform EnemyManager);
	int Health { get; set; }
	void KillSelf();
	void DestroyEffect();
	LockOn lockOn { get; set; }
	Score score { get; set; }
}