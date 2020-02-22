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
		if (bezierWalker.NormalizedT == 1)
		{
			UnityEngine.AddressableAssets.Addressables.ReleaseInstance(bezierWalker.spline.gameObject);
			gameObject.GetComponent<IEnemy>().KillSelf();
			UnityEngine.AddressableAssets.Addressables.ReleaseInstance(this.gameObject);
		}
	}
}