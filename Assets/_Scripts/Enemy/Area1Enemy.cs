using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;

public class Area1Enemy : MonoBehaviour
{
	[SerializeField] AssetReference normalEnemy, multiEnemy;
	void Start()
	{
		StartCoroutine(Area1EnemyPatern());
	}
	IEnumerator Area1EnemyPatern()
	{
		while (true)
		{
			for (int i = -4; i < 5; i++)
				Normal(new Vector3(i * 2, 0, 10));
			Multi(new Vector3(0, 5, 10), 30);
			yield return new WaitForSeconds(20f);
		}
	}
	void Normal(Vector3 pos)
	{
		Addressables.InstantiateAsync(normalEnemy).Completed += op =>
		{
			op.Result.GetComponent<IEnemy>().Initialize(pos, transform);
		};
	}
	void Multi(Vector3 pos, int Health)
	{
		Addressables.InstantiateAsync(multiEnemy).Completed += op =>
		{
			op.Result.GetComponent<IEnemy>().Initialize(pos, transform);
			op.Result.GetComponent<MultiLockEnemy>().Health = Health;
		};
	}
}