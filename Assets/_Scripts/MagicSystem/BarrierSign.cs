using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class BarrierSign : MonoBehaviour
{
	[SerializeField] HandSignDetector detector = default;
	[SerializeField] float ShotInterval = 0.1f;
	[SerializeField] AssetReference BarrierBox = default;
	LockOn lockOn;
	bool Shot = false, IsShooting = false;
	void Start()
	{
		Initialize();
	}
	void Initialize()
	{
		if (detector == null)
		{
			this.enabled = false;
			return;
		}
		lockOn = FindObjectOfType<LockOn>();

		StartCoroutine(ShotLoop());
	}
	void Update()
	{
		Shot = detector.sign == HandSign_Bend.Peace;
	}
	IEnumerator ShotLoop()
	{
		while (true)
		{
			if (Shot && !IsShooting && lockOn.LockedEnemys.Count > 0)
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
				IsShooting = false;
			}
			yield return new WaitForSeconds(ShotInterval);
		}
	}
}
