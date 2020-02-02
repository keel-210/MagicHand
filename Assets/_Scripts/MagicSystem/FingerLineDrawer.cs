using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class FingerLineDrawer : MonoBehaviour
{
	[SerializeField] HandSignDetector detector;
	[SerializeField, Range(0, 0.5f)] float DrawUpdateTime = 0.02f, DrawEndWaitTime = 0.2f, DrawWidth = 0.01f;
	[SerializeField] AssetReference DrawObjectAsset;
	OVRSkeleton _ovrSkelton;
	GameObject DrawObject;
	bool IsDrawing = false, IsEndWait = false;
	void Start()
	{
		if (detector == null)
			this.enabled = false;
		_ovrSkelton = detector.gameObject.GetComponent<OVRSkeleton>();
	}

	void Update()
	{
		if (detector.HandSignNum == (int)HandSign_Bend.Point && !IsDrawing)
			DrawInit();
		else if (DrawObject != null && detector.HandSignNum != (int)HandSign_Bend.Point && !IsEndWait)
			StartCoroutine(DrawEndWait(DrawEndWaitTime));
	}
	void DrawInit()
	{
		Addressables.LoadAssetAsync<GameObject>(DrawObjectAsset).Completed += op =>
		{
			DrawObject = op.Result;
			DrawObject.GetComponent<LineShapeRecognizer>().Initialize(_ovrSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform);
			LineRenderer l = DrawObject.GetComponent<LineRenderer>();
			IsDrawing = true;
			l.positionCount = 1;
			l.SetPosition(0, _ovrSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position);

			StartCoroutine(DrawPointUpdate(l, DrawUpdateTime));
		};
	}
	IEnumerator DrawPointUpdate(LineRenderer line, float waittime)
	{
		while (IsDrawing)
		{
			if (!IsEndWait)
			{
				line.positionCount++;
				line.SetPosition(line.positionCount - 1, _ovrSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position);
			}
			yield return new WaitForSeconds(waittime);
		}
	}
	IEnumerator DrawEndWait(float waitime)
	{
		float t = Time.time;
		IsEndWait = true;

		while (Time.time < t + waitime)
		{
			if (detector.HandSignNum == (int)HandSign_Bend.Point)
			{
				IsEndWait = false;
				yield break;
			}
			yield return null;
		}

		DrawObject.GetComponent<LineShapeRecognizer>()?.DrawEnd();
		DrawObject = null;
		IsDrawing = false;
		IsEndWait = false;
	}
}
