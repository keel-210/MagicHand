using UnityEngine;
public class AutoRelease : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(this.DelayMethod(3f, () => { UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject); }));
	}
}