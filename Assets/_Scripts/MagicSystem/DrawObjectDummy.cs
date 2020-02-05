using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class DrawObjectDummy : MonoBehaviour
{
	[SerializeField] AssetReference reference;
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
		Addressables.ReleaseInstance(Object);
		this.enabled = false;
	}
}
