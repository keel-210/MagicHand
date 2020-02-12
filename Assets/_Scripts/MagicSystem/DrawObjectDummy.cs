using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class DrawObjectDummy : MonoBehaviour
{
	[SerializeField] AssetReference reference = default;
	// Start is called before the first frame update
	GameObject Object;
	void Start()
	{
		Addressables.InstantiateAsync(reference).Completed += op =>
		{
			Object = op.Result;
			Object.GetComponent<LineRenderer>().enabled = false;
			Object.GetComponent<LineShapeRecognizer>().enabled = false;
			Object.GetComponent<ShapeDetector.Detector>().enabled = false;
		};
	}
	void Update()
	{
		if (Object != null)
			Addressables.ReleaseInstance(Object);
		this.enabled = false;
	}
}
