using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class TestBarrier : MonoBehaviour
{
	[SerializeField] float ShotInterval = 0.1f;
	[SerializeField] AssetReference BarrierBox = default;
	LockOn lockOn;
	bool IsShooting;
	void Start()
	{
		lockOn = FindObjectOfType<LockOn>();

		StartCoroutine(ShotLoop());
	}
	void Update()
	{
		IsShooting = Input.GetKey(KeyCode.B);
	}
	IEnumerator ShotLoop()
	{
		while (true)
		{
			if (IsShooting && lockOn.LockedEnemys.Count > 0)
			{
				int BulletCount = lockOn.LockedEnemys.Count;
				Debug.Log("Barrier [x" + BulletCount.ToString() + "]");
				for (int i = 0; i < BulletCount; i++)
				{
					float pitch = 1f + 0.1f * i;
					Addressables.InstantiateAsync(BarrierBox).Completed += op =>
					{
						if (lockOn.LockedEnemys.Count > 0)
						{
							op.Result.GetComponent<BarrierBox>()?.Initialize(lockOn.LockedEnemys[0], pitch);
							lockOn.LockedEnemys.RemoveAt(0);
						}
					};
					yield return new WaitForSeconds(ShotInterval);
				}
			}
			yield return new WaitForSeconds(ShotInterval);
		}
	}
}
