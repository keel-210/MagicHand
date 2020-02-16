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
	IEnumerator ShotLoop()
	{
		while (true)
		{
			if (detector.sign == HandSign_Bend.Peace && lockOn.LockedEnemys.Count > 0)
			{
				int BulletCount = lockOn.LockedEnemys.Count;
				Debug.Log("Shot [x" + BulletCount.ToString() + "]");
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
