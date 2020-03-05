using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;

public class SnakeSlave : EnemyBase
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
		var bezier = transform.parent.GetComponent<BezierSolution.BezierWalkerLocomotion>();
		bezier?.Tail.Remove(transform);
		bezier?.TailDistances.RemoveAt(0);
		StartCoroutine(DestroyScale(0.1f));
	}
	IEnumerator DestroyScale(float waitTime)
	{
		Destroy(GetComponent<ChangeScale>());
		float t = Time.time;
		while (Time.time < t + waitTime)
		{
			transform.localScale = Vector3.one * DestroyScaleCurve.Evaluate((Time.time - t) / waitTime);
			yield return null;
		}
	}
	public void DestroySlave(float waitTime)
	{
		KillSelf();
		StartCoroutine(this.DelayMethod(waitTime, () =>
		{
			DestroyWithScore();
		}));
	}
}