using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
public class TestAreaEnemy : MonoBehaviour
{
	[SerializeField] AssetReference normalEnemy = default, MultiEnemy = default;
	void Start()
	{
		StartCoroutine(TestEnemy());
	}
	IEnumerator TestEnemy()
	{
		while (true)
		{

			for (int i = -4; i < 5; i++)
			{
				Vector3 pos = new Vector3(i * 2, 0, 10);
				Addressables.InstantiateAsync(normalEnemy).Completed += op =>
				{
					op.Result.GetComponent<IEnemy>().Initialize(pos, transform);
				};
			}
			Addressables.InstantiateAsync(MultiEnemy).Completed += op =>
			{
				op.Result.GetComponent<IEnemy>().Initialize(new Vector3(0, 5, 10), transform);
			};
			yield return new WaitForSeconds(20f);
		}
	}
}