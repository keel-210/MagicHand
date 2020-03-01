using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class TestGun : MonoBehaviour
{
	[SerializeField] float ShotInterval = 0.1f;
	[SerializeField] AssetReference BulletAsset = default;
	[SerializeField] AudioSource MaxShot;
	public float DirDot;
	Vector3 IndexDirection;
	GameObject Circle;
	LockOn lockOn;
	bool IsShooting;
	void Start()
	{
		lockOn = FindObjectOfType<LockOn>();

		StartCoroutine(ShotLoop());
	}
	void Update()
	{
		IsShooting = Input.GetKey(KeyCode.S);
	}
	IEnumerator ShotLoop()
	{
		while (true)
		{
			if (IsShooting && lockOn.LockedEnemys.Count > 0)
			{
				int BulletCount = lockOn.LockedEnemys.Count;
				Debug.Log("Shot [x" + BulletCount.ToString() + "]");
				for (int i = 0; i < BulletCount; i++)
				{
					IndexDirection = new Vector3(Random.value, Random.value, Random.value);
					float pitch = 1f + 0.1f * i;
					Addressables.InstantiateAsync(BulletAsset).Completed += op =>
					{
						if (lockOn.LockedEnemys.Count > 0)
						{
							op.Result.GetComponent<Bullet>()?.Initialize(transform.position, IndexDirection, lockOn.LockedEnemys[0], pitch);
							lockOn.LockedEnemys.RemoveAt(0);
						}
					};
					if (i == 9)
						MaxShot.Play();
					yield return new WaitForSeconds(ShotInterval);
				}
			}
			yield return new WaitForSeconds(ShotInterval);
		}
	}
}
