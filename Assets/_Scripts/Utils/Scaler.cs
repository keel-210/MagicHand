using UnityEngine;

public class Scaler : MonoBehaviour
{
	[SerializeField] AnimationCurve curveX = default, curveY = default, curveZ = default;
	[SerializeField] float AnimationTime = default;
	void Update()
	{
		float t = Mathf.Repeat(Time.time, AnimationTime) / AnimationTime;
		transform.localScale = new Vector3(curveX.Evaluate(t), curveY.Evaluate(t), curveZ.Evaluate(t));
	}
}