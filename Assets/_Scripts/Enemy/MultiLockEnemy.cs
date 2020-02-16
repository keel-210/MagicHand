using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;

public class MultiLockEnemy : MonoBehaviour, IEnemy
{
	[SerializeField] AssetReference DestroyEffect_ref = default;
	[SerializeField] int _health = 1;
	[SerializeField] AnimationCurve DestroyScaleCurve = default;
	public int Health { get; set; }
	void Start()
	{
		transform.root.GetComponent<EnemyManagement>().SetMultiEnemy(gameObject);
		Health = _health;
	}
	public void KillSelf()
	{
		transform.root.GetComponent<EnemyManagement>().RemoveMultiEnemy(gameObject);
	}
	void Update()
	{
		if (Health <= 0)
			KillSelf();
	}
	public void DestroyEffect()
	{
		if (Health > 0)
			return;
		KillSelf();
		Vector3 pos = transform.position;
		Addressables.InstantiateAsync(DestroyEffect_ref).Completed += op =>
		{
			op.Result.transform.position = pos;
		};
		StartCoroutine(DestroyScale(0.1f));
	}
	IEnumerator DestroyScale(float waitTime)
	{
		float t = Time.time;
		while (Time.time < t + waitTime)
		{
			transform.localScale = Vector3.one * DestroyScaleCurve.Evaluate((Time.time - t) / waitTime);
			yield return null;
		}
		Addressables.ReleaseInstance(this.gameObject);
	}
}