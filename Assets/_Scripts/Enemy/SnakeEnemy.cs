using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
public class SnakeEnemy : MultiEnemyBase
{
	List<SnakeSlave> enemys;
	void Start()
	{
		enemys = GetComponent<BezierSolution.BezierWalkerLocomotion>().Tail.Select(x => x.GetComponent<SnakeSlave>()).ToList();
		foreach (SnakeSlave s in enemys)
			s.transform.parent = transform;
		OnDestroyWithScore.AddListener(MultiEnemyDestroyWithScore);
		OnDestroyWithoutScore.AddListener(MultiEnemyDestroyWithoutScore);
	}
	void MultiEnemyDestroyWithScore()
	{
		if (Health > 0)
			return;
		KillSelf();
		score.score += 500 * _health;
		Vector3 pos = transform.position;
		Addressables.InstantiateAsync(DestroyEffect_ref).Completed += op =>
		{
			op.Result.transform.position = pos;
		};
		if (lockOn && lockOn.LockedEnemys.Contains(transform))
			lockOn.LockedEnemys.Remove(transform);
	}
	void MultiEnemyDestroyWithoutScore()
	{
		DestroySnakeSlaves();
	}
	void DestroySnakeSlaves()
	{
		int i = 1;
		foreach (SnakeSlave e in GetComponent<BezierSolution.BezierWalkerLocomotion>().Tail.Select(x => x.GetComponent<SnakeSlave>()).ToList())
		{
			e.DestroySlave(0.05f * i);
			i++;
		}
		StartCoroutine(DestroyScale(0.1f));
	}
	public IEnumerator DestroyScale(float waitTime)
	{
		float t = Time.time;
		while (Time.time < t + waitTime)
		{
			transform.localScale = Vector3.one * DestroyScaleCurve.Evaluate((Time.time - t) / waitTime);
			yield return null;
		}
	}
}
