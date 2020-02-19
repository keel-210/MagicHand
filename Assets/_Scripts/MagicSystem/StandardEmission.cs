using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class StandardEmission : MonoBehaviour
{
	[SerializeField] AnimationCurve curve;
	[SerializeField] float AnimationLength;
	[SerializeField] Renderer target;
	[SerializeField] Color emissionColor;
	Material material;
	void Start()
	{
		material = target.sharedMaterial;
	}
	void Update()
	{
		material.SetColor("_EmissionColor", emissionColor * 255 * curve.Evaluate(Mathf.Repeat(Time.time, AnimationLength) / AnimationLength));
	}
	void Disabled()
	{
		material.SetColor("_EmissionColor", emissionColor * 255);
	}
}