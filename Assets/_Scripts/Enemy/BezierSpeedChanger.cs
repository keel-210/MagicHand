using UnityEngine;

public class BezierSpeedChanger : MonoBehaviour
{
	[SerializeField] public AnimationCurve curve;
	[SerializeField] public float curveRatio;
	BezierSolution.BezierWalkerWithSpeed bezierWalker;
	void Start()
	{
		bezierWalker = GetComponent<BezierSolution.BezierWalkerWithSpeed>();
	}
	void Update()
	{
		bezierWalker.speed = curveRatio * curve.Evaluate(bezierWalker.NormalizedT);
	}
}