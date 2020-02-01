using UnityEngine;
using System.Collections;

public class HandSignDetector : MonoBehaviour
{
	[SerializeField, Range(-1, 1)] float ThumbBendThrethold;
	[SerializeField, Range(-1, 1)] float IndexBendThrethold;
	[SerializeField, Range(-1, 1)] float MiddleBendThrethold;
	[SerializeField, Range(-1, 1)] float RingBendThrethold;
	[SerializeField, Range(-1, 1)] float PinkyBendThrethold;

	HandBoneDot handBone;
	public int HandSignNum;
	public HandSign_Bend sign;
	public float DrawEndWaitTime = 0.2f;
	bool IsPosing = false, PoseChangeWaiting = false;
	void Start()
	{
		handBone = GetComponent<HandBoneDot>();
		if (handBone == null)
			return;
	}
	void Update()
	{
		HandSignNum = 0;
		for (int i = 0; i < handBone._fingerDots.Count; i++)
			if (handBone._fingerDots[i].dot > ThumbBendThrethold)
				HandSignNum += (int)Mathf.Pow(2, i);

		if (HandSignNum == (int)sign && !IsPosing)
			UpdateSignInit();
		else if (HandSignNum != (int)sign && !PoseChangeWaiting)
			StartCoroutine(PoseEndWait(DrawEndWaitTime));
	}
	void UpdateSignInit()
	{
		sign = (HandSign_Bend)System.Enum.ToObject(typeof(HandSign_Bend), HandSignNum);
		IsPosing = true;
	}
	IEnumerator PoseEndWait(float waitime)
	{
		float t = Time.time;
		PoseChangeWaiting = true;

		while (Time.time < t + waitime)
		{
			if (HandSignNum == (int)sign)
			{
				PoseChangeWaiting = false;
				yield break;
			}
			yield return null;
		}

		IsPosing = false;
		PoseChangeWaiting = false;
	}
}