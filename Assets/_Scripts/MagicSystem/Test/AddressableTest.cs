using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class AddressableTest : MonoBehaviour
{
	[SerializeField] AssetReference reference = default;
	// Start is called before the first frame update
	GameObject Object;
	void Start()
	{
		Debug.Log("run");
		Addressables.InstantiateAsync(reference).Completed += op =>
		{
			Object = op.Result;
		};
	}
	void Update()
	{
		Addressables.ReleaseInstance(Object);
		this.enabled = false;
	}
}
