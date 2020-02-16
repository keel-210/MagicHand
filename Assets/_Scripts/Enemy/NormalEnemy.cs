using UnityEngine;
using UnityEngine.AddressableAssets;
public class NormalEnemy : MonoBehaviour, IEnemy
{
	[SerializeField] AssetReference DestroyEffect_ref = default;
	public int Health { get; set; }
	void Start()
	{
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
		Addressables.ReleaseInstance(this.gameObject);
	}
}