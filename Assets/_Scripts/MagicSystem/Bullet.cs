using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
public class Bullet : MonoBehaviour
{
	[SerializeField] AssetReference HitEffect = default;
	Rigidbody rigidbody;
	Transform target;
	public void Initialize(Vector3 StartPos, Vector3 IndexDirection, Transform _target)
	{
		transform.position = StartPos;
		rigidbody = gameObject.GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.velocity = IndexDirection * 10f;
		target = _target;
	}
	void FixedUpdate()
	{
		if (!target)
			UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject);

		rigidbody.velocity = (target.position - transform.position).normalized * 10f;
	}
	void OnCollisionEnter(Collision other)
	{
		other.gameObject.GetComponent<IEnemy>()?.DestroyEffect();

		Vector3 pos = transform.position;
		Addressables.InstantiateAsync(HitEffect).Completed += op =>
		{
			op.Result.transform.position = pos;
		};
		UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject);
	}
}