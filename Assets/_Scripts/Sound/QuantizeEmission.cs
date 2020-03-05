using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
public class QuantizeEmission : MonoBehaviour
{
	[SerializeField] Material material;
	[SerializeField] Color emissionColor;
	[SerializeField] float Rate = 1.2f, Time = 0.2f;
	Color color;
	void Update()
	{
		if (Music.IsPlaying && Music.IsJustChangedBar())
		{
			material.DOColor(emissionColor, Time);
			if (color == emissionColor)
				color = emissionColor * 255 * Rate;
			else
				color = emissionColor * 255;
		}
	}
	void Disabled()
	{
		material.SetColor("_EmissionColor", emissionColor * 255);
	}
}