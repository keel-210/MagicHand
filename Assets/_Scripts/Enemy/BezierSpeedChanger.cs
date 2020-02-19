using UnityEngine;

public class BezierSpeedChanger : MonoBehaviour
{
	[SerializeField] AnimationCurve curve;
	BezierSolution.BezierWalkerWithSpeed bezierWalker;
	float t;
	void Start()
	{
		t = Time.time;
	}
	void Update()
	{
		bezierWalker.speed = curve.Evaluate(Time.time - t);
	}
}