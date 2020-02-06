using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class HandSignShift : MonoBehaviour
{
	[SerializeField] public HandSign_Bend FirstSign, SecondSign;
	[SerializeField] HandSignDetector detector;
	[SerializeField] float WaitTime;
	public class ShiftDetectEvent : UnityEvent { }
	public ShiftDetectEvent onShiftDetected = new ShiftDetectEvent();
	HandSign_Bend PrevSign;
	bool IsShifting;
	void Update()
	{
		if (PrevSign == FirstSign && detector.sign != FirstSign && !IsShifting)
			ShiftWait();
		PrevSign = detector.sign;
	}
	void ShiftWait()
	{
		IsShifting = true;
		StartCoroutine(ShiftWaitCoroutine());
	}
	IEnumerator ShiftWaitCoroutine()
	{
		Debug.Log("Shifting...");
		float t = Time.time;
		while (Time.time < t + WaitTime)
		{
			if (detector.sign == SecondSign)
			{
				Debug.Log("Shift!");
				onShiftDetected.Invoke();
				IsShifting = false;
				yield break;
			}
			yield return null;
		}
		IsShifting = false;
	}
}