using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AddressableAssets;
public class LineShapeRecognizer : MonoBehaviour
{
	[SerializeField] AssetReference MagicCircle;
	public void Initialize(Transform IndexTip)
	{
		GetComponent<ShapeDetector.Detector>().onShapeDetected.AddListener(OnShapeDetected);
		GetComponent<ShapeDetector.Detector>().Initialize();
		transform.parent = IndexTip;
		transform.localPosition = Vector3.zero;
	}
	public void DrawEnd(Vector3[] positions)
	{
		LineRenderer l = GetComponent<LineRenderer>();
		StartCoroutine(DestroyLine(l, 0.5f));
		if (positions.Length < 20) return;
		GetComponent<ShapeDetector.Detector>().SetPositions(positions);
		GetComponent<ShapeDetector.Detector>().DetectShape();
	}
	public void OnShapeDetected(ShapeDetector.ShapeInfo info)
	{
		Debug.Log(info.type.ToString() + " : " + info.position.ToString("F3"));
		if (info.type == ShapeDetector.ShapeType.Circle)
			Addressables.InstantiateAsync(MagicCircle).Completed += op => { op.Result.GetComponent<MagicCircle>()?.Initialize(info.position); };
	}
	IEnumerator DestroyLine(LineRenderer line, float waitTime)
	{
		float t = Time.time;
		while (Time.time < t + waitTime)
		{
			Color s = line.startColor, e = line.endColor;
			s.a = e.a = 1 - ((Time.time - t) / waitTime) > 0 ? 1 - ((Time.time - t) / waitTime) : 0;
			line.startColor = s;
			line.endColor = e;
			yield return null;
		}
		line.startColor = line.endColor = new Color(0, 0, 0, 0);
		UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject);
	}
}