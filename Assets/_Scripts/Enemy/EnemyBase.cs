using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour, IEnemy
{
	[SerializeField] protected AssetReference DestroyEffect_ref = default;
	[SerializeField] protected AnimationCurve DestroyScaleCurve = default;
	public int Health { get; set; }
	public LockOn lockOn { get; set; }
	public Score score { get; set; }
	public UnityEvent OnDestroyWithScore { get; set; } = new UnityEvent();
	public UnityEvent OnDestroyWithoutScore { get; set; } = new UnityEvent();
	public AreaEnemy.EnemyMovement EnemyMovement { get; set; }

	public void Initialize(AreaEnemy.EnemyMovement e)
	{
		EnemyMovement = e;
		transform.parent = e.Manager.transform;
		transform.localPosition = e.EnemyPos;
		transform.root.GetComponent<EnemyManagement>().SetEnemy(gameObject);
		Health = 1;
		if (GetComponent<BezierSolution.BezierWalkerWithSpeed>())
			GetComponent<BezierSolution.BezierWalkerWithSpeed>().spline = e.Bezier.GetComponent<BezierSolution.BezierSpline>();
		if (GetComponent<BezierSpeedChanger>())
		{
			GetComponent<BezierSpeedChanger>().curve = e.SpeedCurve;
			GetComponent<BezierSpeedChanger>().curveRatio = e.SpeedRatio;
		}
	}
	public void KillSelf()
	{
		if (gameObject)
			transform.root.GetComponent<EnemyManagement>().RemoveEnemy(gameObject);
	}
	public void DestroyWithScore()
	{
		OnDestroyWithScore.Invoke();
		DestroyWithoutScore();
	}
	public void DestroyWithoutScore()
	{
		lockOn.RemoveMe(transform);
		OnDestroyWithoutScore.Invoke();
		StartCoroutine(this.DelayMethod(1f, () =>
		{
			if (!Addressables.ReleaseInstance(this.gameObject))
				Destroy(gameObject);
		}));
	}
}