using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BackGroundActivater : MonoBehaviour
{
	[SerializeField] Transform player;
	[SerializeField] float[] AreaLimitZ;
	[SerializeField] List<GameObject> BackgroundObj = new List<GameObject>();
	int nowActiveObjNum = 0;
	void Start()
	{
		BackgroundObj.ForEach(x => x.SetActive(false));
		BackgroundObj[0].SetActive(true);
	}
	void Update()
	{
		if (player.position.z > AreaLimitZ[nowActiveObjNum + 1])
		{
			BackgroundObj[nowActiveObjNum].SetActive(false);
			BackgroundObj[nowActiveObjNum + 1].SetActive(true);
			nowActiveObjNum++;
			if (nowActiveObjNum == BackgroundObj.Count)
				this.enabled = false;
		}
	}
}