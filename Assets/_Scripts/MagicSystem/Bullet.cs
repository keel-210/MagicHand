using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
public class Bullet : MonoBehaviour
{
	[SerializeField] AssetReference HitEffect = default;
	[SerializeField] float HitTime = 0.1f;
	Rigidbody rigidbody;
	public Transform target;
	public bool IsInitialized = false, IsHit = false;
	public void Initialize(Vector3 StartPos, Vector3 IndexDirection, Transform _target)
	{
		transform.position = StartPos;
		rigidbody = gameObject.GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.velocity = IndexDirection * 10f;
		Quantize.QuantizePlay(GetComponent<AudioSource>());
		target = _target;
		IsInitialized = true;
		IsHit = false;
	}
	void FixedUpdate()
	{
		if (!IsInitialized)
			return;
		if (!target || !transform)
		{
			UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject);
			IsHit = true;
		}
		if (IsHit)
			return;
		Vector3 a = 2f * ((target.position - transform.position) - (rigidbody.velocity * HitTime)) / (HitTime * HitTime);
		rigidbody?.AddForce(a, ForceMode.Acceleration);
		if (HitTime > 0)
			HitTime -= Time.deltaTime;
		if (HitTime <= 0)
			HitTime = 0.01f;
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.transform != target)
			return;
		other.gameObject.GetComponent<IEnemy>()?.DestroyWithScore();
		IsHit = true;
		Vector3 pos = transform.position;
		Addressables.InstantiateAsync(HitEffect).Completed += op =>
		{
			op.Result.transform.position = pos;
		};
		UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject);
	}
}