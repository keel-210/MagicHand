using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class TestGun : MonoBehaviour
{
	[SerializeField] float ShotInterval = 0.1f;
	[SerializeField] AssetReference BulletAsset = default, BulletSound = default;
	[SerializeField] AudioSource MaxShot;
	public float DirDot;
	Vector3 IndexDirection;
	GameObject Circle;
	LockOn lockOn;
	bool Shot = false, IsShooting = false;
	void Start()
	{
		lockOn = FindObjectOfType<LockOn>();

		StartCoroutine(ShotLoop());
	}
	void Update()
	{
		Shot = Input.GetKey(KeyCode.S);
	}
	IEnumerator ShotLoop()
	{
		while (true)
		{
			if (Shot && !IsShooting && lockOn.LockedEnemys.Count > 0)
			{
				int BulletCount = lockOn.LockedEnemys.Count;
				Debug.Log("Shot [x" + BulletCount.ToString() + "]");
				Addressables.InstantiateAsync(BulletSound).Completed += op =>
				{
					op.Result.GetComponent<BulletSound>().Initialize(BulletCount);
				};
				IsShooting = true;
				for (int i = 0; i < BulletCount; i++)
				{
					IndexDirection = new Vector3(Random.value, Random.value, Random.value);
					Addressables.InstantiateAsync(BulletAsset).Completed += op =>
					{
						if (lockOn.LockedEnemys.Count > 0)
						{
							op.Result.GetComponent<Bullet>()?.Initialize(transform.position, IndexDirection, lockOn.LockedEnemys[0]);
							lockOn.LockedEnemys.RemoveAt(0);
						}
					};
					if (i == lockOn.LockOnLimit - 1)
						Quantize.QuantizePlay(MaxShot);
					yield return new WaitForSeconds(ShotInterval);
				}
				IsShooting = false;
			}
			yield return new WaitForSeconds(ShotInterval);
		}
	}
}
