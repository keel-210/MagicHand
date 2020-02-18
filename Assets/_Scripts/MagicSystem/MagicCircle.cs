using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using System.Collections;
public class MagicCircle : MonoBehaviour
{
	bool IsEnhanced;
	public void Initialize(Vector3 pos, Quaternion rot)
	{
		transform.position = pos;
		transform.rotation = rot;
		var shifts = FindObjectsOfType<HandSignShift>().Where(x => x.FirstSign == HandSign_Bend.Fist && x.SecondSign == HandSign_Bend.OpenHand);
		foreach (var s in shifts)
			s.onShiftDetected.AddListener(Strong_Circle);
		Release_Object(20f);
	}
	public void Release_Object(float WaitTime)
	{
		StartCoroutine(this.DelayMethod(WaitTime, () =>
		{
			if (!IsEnhanced)
			{
				var shifts = FindObjectsOfType<HandSignShift>().Where(x => x.FirstSign == HandSign_Bend.Fist && x.SecondSign == HandSign_Bend.OpenHand);
				foreach (var s in shifts)
					s.onShiftDetected.RemoveListener(Strong_Circle);
				Addressables.ReleaseInstance(gameObject);
			}
		}));
	}
	void Strong_Circle()
	{
		StartCoroutine(Strong_Coroutine());
	}
	IEnumerator Strong_Coroutine()
	{
		Debug.Log("Magic Circle Enhanced.");
		float t = Time.time;
		Vector3 StartSize = transform.localScale;
		while (Time.time < t + 0.5f)
		{
			transform.localScale = StartSize + Vector3.one * 1.4f * (Time.time - t) * (Time.time - t);
			transform.rotation *= Quaternion.Euler(Vector3.forward * (Time.time - t) * (Time.time - t) * 180f);
			yield return null;
		}
		var shifts = FindObjectsOfType<HandSignShift>().Where(x => x.FirstSign == HandSign_Bend.Fist && x.SecondSign == HandSign_Bend.OpenHand);
		foreach (var s in shifts)
			s.onShiftDetected.RemoveListener(Strong_Circle);
	}
}