using UnityEngine;

public class MagicTriangle : MonoBehaviour
{
	public void Initialize(Vector3 pos)
	{
		transform.position = pos;
		StartCoroutine(this.DelayMethod(3f, () => { UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject); }));
	}
	void Start()
	{

	}
}