using UnityEngine;

public class SnakeSlave : MonoBehaviour
{
	void OnDisable()
	{
		var bezier = transform.parent.GetComponent<BezierSolution.BezierWalkerLocomotion>();
		if (bezier.Tail.Contains(transform))
			bezier.Tail.Remove(transform);
		if (bezier.TailDistances.Count > 0)
			bezier.TailDistances.RemoveAt(0);
	}
}