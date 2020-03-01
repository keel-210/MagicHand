using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
using UnityEngine.Events;

public class MultiLockEnemy : MultiEnemyBase
{
	void Start()
	{
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