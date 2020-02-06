using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using System.Collections;
public class MagicCircle : MonoBehaviour
{
	bool IsStrongethed;
	public void Initialize(Vector3 pos)
	{
		transform.position = pos;
		var shifts = FindObjectsOfType<HandSignShift>().Where(x => x.FirstSign == HandSign_Bend.Fist && x.SecondSign == HandSign_Bend.OpenHand);
		Debug.Log("Shift Length : " + shifts.Count());
		foreach (var s in shifts)
			s.onShiftDetected.AddListener(Strong_Circle);
	}
	void Strong_Circle()
	{
		if (!IsStrongethed)
			StartCoroutine(Strong_Coroutine());
	}
	IEnumerator Strong_Coroutine()
	{
		Debug.Log("Strong Magic Circle!");
		float t = Time.time;
		while (Time.time < t + 0.5f)
		{
			transform.localScale = Vector3.one * 0.3f + Vector3.one * 1.4f * (Time.time - t) * (Time.time - t);
			transform.rotation *= Quaternion.Euler(Vector3.forward * (Time.time - t) * (Time.time - t) * 180f);
			yield return null;
		}
		IsStrongethed = true;
	}
}