using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Gun : MonoBehaviour
{
	[SerializeField] HandSignDetector detector = default;
	[SerializeField] float ShotInterval = 0.1f;
	[SerializeField] AssetReference BulletAsset = default, GunCircle = default;
	public float DirDot;
	OVRSkeleton _ovrSkelton;
	Transform IndexTip, Index3;
	Vector3 IndexDirection;
	GameObject Circle;
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
		_ovrSkelton = detector.GetComponent<OVRSkeleton>();
		lockOn = FindObjectOfType<LockOn>();


		if (_ovrSkelton == null)
			return;

		IndexTip = _ovrSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform;
		Index3 = _ovrSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_Index3].Transform;

		StartCoroutine(ShotLoop());
		Addressables.InstantiateAsync(GunCircle).Completed += op =>
		{
			op.Result.transform.parent = transform;
			op.Result.transform.position = IndexTip.position;
			Circle = op.Result;
		};
	}
	void Update()
	{
		if (_ovrSkelton == null)
			Initialize();
		if (Circle != null)
		{
			Circle.transform.position = IndexTip.position;
			Circle.transform.rotation = Quaternion.FromToRotation(Vector3.forward, (IndexTip.position - Index3.position).normalized);
			Circle.SetActive(detector.sign == HandSign_Bend.Gun);
		}
	}
	IEnumerator ShotLoop()
	{
		while (true)
		{
			if (detector.sign == HandSign_Bend.Gun && lockOn.LockedEnemys.Count > 0)
			{
				int BulletCount = lockOn.LockedEnemys.Count;
				Debug.Log("Shot [x" + BulletCount.ToString() + "]");
				for (int i = 0; i < BulletCount; i++)
				{
					IndexDirection = (IndexTip.position - Index3.position).normalized;
					float pitch = 1f + 0.1f * i;
					Addressables.InstantiateAsync(BulletAsset).Completed += op =>
					{
						if (lockOn.LockedEnemys.Count > 0)
						{
							op.Result.GetComponent<Bullet>()?.Initialize(IndexTip.position, IndexDirection, lockOn.LockedEnemys[0], pitch);
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
