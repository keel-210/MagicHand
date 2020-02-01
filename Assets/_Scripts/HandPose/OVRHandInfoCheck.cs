using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRHandInfoCheck : MonoBehaviour
{
	void Start()
	{
		var skeleton = GetComponent<OVRSkeleton>();
		Debug.Log(skeleton.Bones);
	}

	void Update()
	{

	}
}
