using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class StandardEmission : MonoBehaviour
{
	[SerializeField] AnimationCurve curve;
	[SerializeField] float AnimationLength;
	[SerializeField] Material target;
	[SerializeField] Color emissionColor;
	void Update()
	{
		target.SetColor("_EmissionColor", emissionColor * 255 * curve.Evaluate(Mathf.Repeat(Time.time, AnimationLength) / AnimationLength));
	}
	void Disabled()
	{
		target.SetColor("_EmissionColor", emissionColor * 255);
	}
}