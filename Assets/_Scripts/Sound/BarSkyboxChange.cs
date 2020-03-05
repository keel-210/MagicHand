using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BarSkyboxChange : MonoBehaviour
{
	[SerializeField] Color[] colors;
	[SerializeField] Material material;
	public float Time = 0.2f;
	int index = 0;
	float r, g, b, a;
	void Update()
	{
		if (Music.IsPlaying && Music.IsJustChangedBar())
		{
			ColorTintChange(colors[index], Time);
			index++;
			if (colors.Length == index)
				index = 0;
		}
		material.SetColor("_Tint", new Color(r, g, b, a));
	}
	void ColorTintChange(Color c, float time)
	{
		DOTween.To(() => r, num => r = num, c.r, time);
		DOTween.To(() => g, num => g = num, c.g, time);
		DOTween.To(() => b, num => b = num, c.b, time);
		DOTween.To(() => a, num => a = num, c.a, time);
	}
}