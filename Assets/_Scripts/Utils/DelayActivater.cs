using UnityEngine;
using System.Collections.Generic;

public class DelayActivater : MonoBehaviour
{
	[SerializeField] float DelayTime = default;
	[SerializeField] List<GameObject> ObjectList = new List<GameObject>();
	void Start()
	{
		StartCoroutine(this.DelayMethod(DelayTime, () =>
		{
			foreach (GameObject g in ObjectList)
				g.SetActive(true);
		}));
	}
}