using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class FingerDotViewer : MonoBehaviour
{
	[SerializeField] List<Slider> sliders = default;
	HandBoneDot handBone;
	bool _isInitialized;

	private void Awake()
	{
		if (handBone == null)
		{
			handBone = GetComponent<HandBoneDot>();
		}
	}

	private void Start()
	{
		if (handBone == null || sliders.Count == 0)
		{
			this.enabled = false;
			return;
		}

		Initialize();
	}

	private void Initialize()
	{
		handBone = GetComponent<HandBoneDot>();

		_isInitialized = true;
	}

	public void Update()
	{
		if (_isInitialized)
		{
			for (int i = 0; i < handBone._fingerDots.Count; i++)
				sliders[i].value = handBone._fingerDots[i].dot;
		}
	}
}