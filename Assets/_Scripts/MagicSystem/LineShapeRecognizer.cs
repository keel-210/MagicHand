using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class LineShapeRecognizer : MonoBehaviour
{
	public void Recognize()
	{
		LineRenderer l = GetComponent<LineRenderer>();
		if (l == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3[] points = new Vector3[l.positionCount];
		l.GetPositions(points);
		List<Vector3> PointsList = points.ToList();
		CircleRecognize(PointsList);
		StartCoroutine(DestroyLine(l, 0.5f));
	}
	void CircleRecognize(List<Vector3> points)
	{
		Vector3 Ave = new Vector3(points.Select(x => x.x).Average(), points.Select(x => x.y).Average(), points.Select(x => x.z).Average());
		IEnumerable<float> Radiuses = points.Select(x => (x - Ave).magnitude);
		float AveRasius = Radiuses.Average();
		float ErrorSum = Radiuses.Select(x => Mathf.Abs(x - AveRasius)).Sum();
		if (ErrorSum / (points.Count * AveRasius) < 0.1f)
		{
			GameObject obj = new GameObject();
			obj.transform.position = Ave;
			obj.AddComponent<MagicCircle>();
		}
	}
	IEnumerator DestroyLine(LineRenderer line, float waitTime)
	{
		float t = Time.time;
		while (Time.time < t + waitTime)
		{
			Color s = line.startColor, e = line.endColor;
			s.a = e.a = (Time.time - t) / waitTime;
			line.startColor = s;
			line.endColor = e;
			yield return null;
		}
	}
}