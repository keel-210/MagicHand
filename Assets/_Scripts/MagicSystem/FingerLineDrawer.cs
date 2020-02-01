using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerLineDrawer : MonoBehaviour
{
	[SerializeField] HandSignDetector detector;
	[SerializeField, Range(0, 0.5f)] float DrawUpdateTime = 0.02f, DrawEndWaitTime = 0.2f, DrawWidth = 0.01f;
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
		DrawObject = new GameObject();
		LineRenderer l = DrawObject.AddComponent<LineRenderer>();
		DrawObject.AddComponent<LineShapeRecognizer>();

		l.material = new Material(Shader.Find("Diffuse"));
		l.startWidth = DrawWidth;
		l.endWidth = DrawWidth;
		IsDrawing = true;
		l.positionCount = 1;
		l.SetPosition(0, _ovrSkelton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position);

		StartCoroutine(DrawPointUpdate(l, DrawUpdateTime));
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

		Debug.Log("Destroyed! : " + System.Enum.GetName(typeof(HandSign_Bend), detector.HandSignNum));
		DrawObject.GetComponent<LineShapeRecognizer>()?.Recognize();
		DrawObject = null;
		IsDrawing = false;
		IsEndWait = false;
	}
}
