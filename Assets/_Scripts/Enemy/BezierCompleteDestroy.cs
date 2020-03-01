using UnityEngine;

public class BezierCompleteDestroy : MonoBehaviour
{
	BezierSolution.BezierWalkerWithSpeed bezierWalker;
	void Start()
	{
		bezierWalker = GetComponent<BezierSolution.BezierWalkerWithSpeed>();
	}
	void Update()
	{
		if (bezierWalker.NormalizedT == 1 && bezierWalker.spline)
		{
			gameObject.GetComponent<IEnemy>().KillSelf();
			gameObject.GetComponent<IEnemy>().DestroyWithoutScore();
			if (bezierWalker.spline)
				UnityEngine.AddressableAssets.Addressables.ReleaseInstance(bezierWalker.spline.gameObject);
			Destroy(bezierWalker);
		}
	}
	void OnDisable()
	{
		if (bezierWalker && bezierWalker.spline)
			UnityEngine.AddressableAssets.Addressables.ReleaseInstance(bezierWalker.spline.gameObject);
	}
}