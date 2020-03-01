using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public interface IEnemy
{
	void Initialize(AreaEnemy.EnemyMovement e);
	int Health { get; set; }
	void KillSelf();
	void DestroyWithScore();
	void DestroyWithoutScore();
	UnityEvent OnDestroyWithScore { get; set; }
	UnityEvent OnDestroyWithoutScore { get; set; }
	LockOn lockOn { get; set; }
	Score score { get; set; }
	AreaEnemy.EnemyMovement EnemyMovement { get; set; }
}