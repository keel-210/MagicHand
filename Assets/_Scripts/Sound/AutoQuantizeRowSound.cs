using UnityEngine;

public class AutoQuantizeRowSound : MonoBehaviour
{
	void Start()
	{
		Quantize.QuantizeRowPlay(GetComponent<AudioSource>());
	}
}