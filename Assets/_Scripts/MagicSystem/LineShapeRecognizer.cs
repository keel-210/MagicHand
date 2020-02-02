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
		transform.parent = IndexTip;
	}
	public void DrawEnd()
	{
		LineRenderer l = GetComponent<LineRenderer>();
		StartCoroutine(DestroyLine(l, 0.5f));
	}
	public void OnShapeDetected(ShapeDetector.ShapeInfo info)
	{
		Debug.Log(info.type + ": " + info.position);
		if (info.type == ShapeDetector.ShapeType.Circle)
			Addressables.LoadAssetAsync<GameObject>(MagicCircle).Completed += op => { op.Result.GetComponent<MagicCircle>()?.Initialize(info.position); };
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
		UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject);
	}
}