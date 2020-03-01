using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;
public class GateEnemy : MonoBehaviour
{
	[SerializeField] List<Transform> slaves = new List<Transform>();
	void Start()
	{
		slaves.ForEach(t => t.parent = transform);
		transform.parent = slaves[0].GetComponent<GateSlave>().EnemyMovement.Manager.transform;
		UnityEngine.AddressableAssets.Addressables.ReleaseInstance(slaves[0].GetComponent<GateSlave>().EnemyMovement.Bezier);
	}
	public void KillSlave(Transform tra)
	{
		slaves.Remove(tra);
	}
	void Update()
	{
		if (slaves.Count == 0)
			Addressables.ReleaseInstance(gameObject);
	}
}