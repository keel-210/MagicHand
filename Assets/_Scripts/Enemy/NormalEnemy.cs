using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
using UnityEngine.Events;

public class NormalEnemy : EnemyBase
{
	void Start()
	{
		OnDestroyWithScore.AddListener(EnemyDestroyWithScore);
		OnDestroyWithoutScore.AddListener(EnemyDestroyWithoutScore);
	}
	void EnemyDestroyWithScore()
	{
		score.score += 500;
		Vector3 pos = transform.position;
		Addressables.InstantiateAsync(DestroyEffect_ref).Completed += op =>
		{
			op.Result.transform.position = pos;
		};
		if (lockOn && lockOn.LockedEnemys.Contains(transform))
			lockOn.LockedEnemys.Remove(transform);
	}
	void EnemyDestroyWithoutScore()
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
		transform.localScale = Vector3.one * 0.001f;
	}
}