using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;

public class GateSlave : EnemyBase
{
	[SerializeField] Vector3 dir, localPos;
	[SerializeField] Transform GatePart;
	[SerializeField] AnimationCurve partCurve;
	void Start()
	{
		OnDestroyWithScore.AddListener(EnemyDestroyWithScore);
		OnDestroyWithoutScore.AddListener(EnemyDestroyWithoutScore);
	}
	void Update()
	{
		transform.localPosition = localPos;
	}
	void EnemyDestroyWithScore()
	{
		score.score += 500;
		Vector3 pos = transform.position;
		Addressables.InstantiateAsync(DestroyEffect_ref).Completed += op =>
		{
			op.Result.transform.position = pos;
		};
		if (lockOn)
			lockOn.LockedEnemys.Remove(transform);
		EnemyDestroyWithoutScore();
	}
	void EnemyDestroyWithoutScore()
	{
		StartCoroutine(DestroyScale(0.2f, 0.2f));
	}
	public IEnumerator DestroyScale(float waitTimePos, float waitTimeScale)
	{
		float t = Time.time;
		while (Time.time < t + waitTimePos)
		{
			float tt = (Time.time - t) / (waitTimePos);
			GatePart.localPosition += dir * partCurve.Evaluate(tt) * 0.3f;
			yield return null;
		}
		while (t + waitTimePos < Time.time && Time.time < t + waitTimePos + waitTimeScale)
		{
			transform.localScale = Vector3.one * DestroyScaleCurve.Evaluate((Time.time - t - waitTimePos) / (waitTimeScale));
			GatePart.localScale = Vector3.one * DestroyScaleCurve.Evaluate((Time.time - t - waitTimePos) / (waitTimeScale));
			yield return null;
		}
		transform.parent.GetComponent<GateEnemy>().KillSlave(transform);
		if (!Addressables.ReleaseInstance(GatePart.gameObject))
			Destroy(GatePart.gameObject);
	}
}
