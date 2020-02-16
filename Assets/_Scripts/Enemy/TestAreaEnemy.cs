using UnityEngine;
using UnityEngine.AddressableAssets;
public class TestAreaEnemy : MonoBehaviour
{
	[SerializeField] AssetReference normalEnemy = default, MultiEnemy = default;
	void Start()
	{
		for (int i = -4; i < 5; i++)
		{
			Vector3 pos = new Vector3(i * 2, 0, 10);
			Addressables.InstantiateAsync(normalEnemy).Completed += op =>
			{
				op.Result.transform.parent = transform;
				op.Result.transform.localPosition = pos;
				op.Result.name = op.Result.transform.position.ToString();
			};
		}
		Addressables.InstantiateAsync(MultiEnemy).Completed += op =>
		{
			op.Result.transform.parent = transform;
			op.Result.transform.localPosition = new Vector3(0, 5, 10);
			op.Result.name = op.Result.transform.position.ToString();
		};
	}
}