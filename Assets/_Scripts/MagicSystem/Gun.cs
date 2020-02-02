using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Gun : MonoBehaviour
{
	[SerializeField] HandSignDetector detector;
	[SerializeField] float DirectoinThrethold = 0.95f, ShotInterval = 0.1f;
	[SerializeField] AssetReference BulletAsset;
	public float DirDot;
	OVRSkeleton _ovrSkelton;
	Transform IndexTip, Index3;
	Vector3 IndexDirection, PrevIndexDirection;
	bool IsShoting;
	void Start()
	{
		if (detector == null)
			this.enabled = false;
		_ovrSkelton = detector.GetComponent<OVRSkeleton>();
		IndexTip = _ovrSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform;
		Index3 = _ovrSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_Index3].Transform;
	}

	void Update()
	{
		IndexDirection = (IndexTip.position - Index3.position).normalized;
		DirDot = Vector3.Dot(PrevIndexDirection, IndexDirection);
		if (detector.sign == HandSign_Bend.Gun && DirDot < DirectoinThrethold && !IsShoting)
		{
			Addressables.LoadAssetAsync<GameObject>(BulletAsset).Completed += op =>
			{
				op.Result.GetComponent<Bullet>()?.Initialize(IndexTip.position, IndexDirection);
			};
			IsShoting = true;
			StartCoroutine(this.DelayMethod(ShotInterval, () => { IsShoting = false; }));
		}
		PrevIndexDirection = IndexDirection;
	}
}
