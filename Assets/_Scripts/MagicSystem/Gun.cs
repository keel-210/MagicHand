using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] HandSignDetector detector;
	OVRSkeleton _ovrSkelton;
	void Start()
	{
		if (detector == null)
			this.enabled = false;
		_ovrSkelton = GetComponent<OVRSkeleton>();
	}

	void Update()
	{
		if (detector.sign == HandSign_Bend.Gun)
		{

		}
	}
}
