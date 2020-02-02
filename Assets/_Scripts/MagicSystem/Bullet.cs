using UnityEngine;

public class Bullet : MonoBehaviour
{
	public void Initialize(Vector3 StartPos, Vector3 IndexDirection)
	{
		transform.localScale = Vector3.one * 0.01f;
		transform.position = StartPos;
		Rigidbody r = gameObject.AddComponent<Rigidbody>();
		r.useGravity = false;
		r.velocity = IndexDirection * 0.1f;
		StartCoroutine(this.DelayMethod(3f, () => { UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject); }));
	}
}