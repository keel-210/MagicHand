using UnityEngine;

public class AutoQuantizeSound : MonoBehaviour
{
	void Start()
	{
		Quantize.QuantizePlay(GetComponent<AudioSource>());
	}
}