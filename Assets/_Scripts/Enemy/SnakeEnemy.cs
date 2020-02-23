using UnityEngine;
using UnityEngine.AddressableAssets;

public class SnakeEnemy : MonoBehaviour
{
	IEnemy[] enemy;
	void Start()
	{
		enemy = GetComponentsInChildren<IEnemy>();
	}
	void OnDisable()
	{
		foreach (IEnemy e in enemy)
			e.DestroyEffect();
	}
}
