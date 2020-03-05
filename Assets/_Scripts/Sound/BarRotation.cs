using UnityEngine;
using DG.Tweening;
public class BarRotation : MonoBehaviour
{
	public float Time = 0.2f;
	int index = 0;
	void Update()
	{
		if (Music.IsPlaying && Music.IsJustChangedBar())
		{
			transform.DOLocalRotate(new Vector3(Random.value, Random.value, Random.value) * 360, Time);
		}
	}
}