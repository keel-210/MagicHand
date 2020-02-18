using UnityEngine;
public class AutoRelease : MonoBehaviour
{
	public float ReleaseTime = 3f;
	void Start()
	{
		StartCoroutine(this.DelayMethod(ReleaseTime, () => { UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject); }));
	}
}