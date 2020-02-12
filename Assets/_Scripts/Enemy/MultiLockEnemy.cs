using UnityEngine;
using UnityEngine.AddressableAssets;

public class MultiLockEnemy : MonoBehaviour, IEnemy
{
	[SerializeField] AssetReference DestroyEffect_ref = default;
	[SerializeField] int _health = 1;
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
		Addressables.InstantiateAsync(DestroyEffect_ref).Completed += op =>
		{
			op.Result.transform.position = transform.position;
		};
		Addressables.ReleaseInstance(gameObject);
	}
}