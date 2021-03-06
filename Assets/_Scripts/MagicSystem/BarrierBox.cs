using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;
public class BarrierBox : MonoBehaviour
{
	[SerializeField, Range(0, 1f)] float LineXTime = default, LineYTime = default, LineZTime = default, FaceXTime = default, FaceYTime = default, FaceZTime = default;
	Material material;
	Transform target;
	public void Initialize(Transform _target, float pitch)
	{
		if (!_target)
		{
			Addressables.ReleaseInstance(gameObject);
			return;
		}
		target = _target;
		transform.position = _target.position + new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * 0.5f;
		material = GetComponent<Renderer>().material;
		StartCoroutine(BarrierBoxInitEffect());
	}
	IEnumerator BarrierBoxInitEffect()
	{
		material.SetFloat("_XLine", -0.5f);
		material.SetFloat("_YLine", -0.49f);
		material.SetFloat("_ZLine", -0.5f);
		material.SetFloat("_XFace", 0.5f);
		material.SetFloat("_YFace", -0.5f);
		material.SetFloat("_ZFace", 0.5f);
		StartCoroutine(ShaderFloatValueChanger(material, "_XLine", -0.5f, 0.5f, LineXTime));
		yield return new WaitForSeconds(LineXTime);
		StartCoroutine(ShaderFloatValueChanger(material, "_ZLine", -0.5f, 0.5f, LineZTime));
		yield return new WaitForSeconds(LineZTime);
		StartCoroutine(ShaderFloatValueChanger(material, "_YLine", -0.49f, 0.5f, LineYTime));
		StartCoroutine(ShaderFloatValueChanger(material, "_YFace", -0.5f, 0.5f, FaceYTime));
		yield return new WaitForSeconds(FaceYTime);
		if (target)
			target.GetComponent<IEnemy>()?.DestroyWithScore();
		yield return new WaitForSeconds(FaceYTime * 2);
		Addressables.ReleaseInstance(gameObject);
	}
	IEnumerator ShaderFloatValueChanger(Material mat, string name, float startValue, float endValue, float changingTime)
	{
		float t = Time.time;
		mat.SetFloat(name, startValue);
		float ValueVelo = (endValue - startValue) / changingTime;
		while (Time.time < t + changingTime)
		{
			mat.SetFloat(name, startValue + ((Time.time - t) * ValueVelo));
			yield return null;
		}
		mat.SetFloat(name, endValue);
	}
}