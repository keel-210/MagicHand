using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
public class NormalEnemy : MonoBehaviour, IEnemy
{
	[SerializeField] AssetReference DestroyEffect_ref = default;
	[SerializeField] AnimationCurve DestroyScaleCurve = default;
	public int Health { get; set; }
	public void Initialize(Vector3 pos, Transform Manager)
	{
		transform.parent = Manager;
		transform.localPosition = pos;
		transform.root.GetComponent<EnemyManagement>().SetEnemy(gameObject);
		Health = 1;
	}
	public void KillSelf()
	{
		transform.root.GetComponent<EnemyManagement>().RemoveEnemy(gameObject);
	}
	void Update()
	{
		if (Health <= 0)
			KillSelf();
	}
	public void DestroyEffect()
	{
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