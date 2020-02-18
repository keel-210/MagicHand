using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class StochasticEmission : MonoBehaviour
{
	[SerializeField] AnimationCurve curve;
	[SerializeField] float AnimationLength;
	List<Material> matList = new List<Material>();
	void Start()
	{
		matList = GetComponentsInChildren<Renderer>().Select(x => x.material).ToList();
	}
	void Update()
	{

	}
}