using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BarColorChange : MonoBehaviour
{
	[SerializeField] Color[] colors;
	[SerializeField] Material material;
	public float Time = 0.2f;
	int index = 0;
	void Update()
	{
		if (Music.IsPlaying && Music.IsJustChangedBar())
		{
			material.DOColor(colors[index], Time);
			index++;
			if (colors.Length == index)
				index = 0;
		}
	}
}