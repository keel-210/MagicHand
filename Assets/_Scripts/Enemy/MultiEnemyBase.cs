using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
using UnityEngine.Events;

public class MultiEnemyBase : MonoBehaviour, IEnemy
{
	[SerializeField] protected AssetReference DestroyEffect_ref = default;
	[SerializeField] protected AnimationCurve DestroyScaleCurve = default;
	[SerializeField] protected int _health;
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
		transform.root.GetComponent<EnemyManagement>().SetMultiEnemy(gameObject);
		Health = e.health;
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
		transform.root.GetComponent<EnemyManagement>().RemoveMultiEnemy(gameObject);
	}
	void Update()
	{
		if (Health <= 0)
			KillSelf();
	}
	public void DestroyWithScore()
	{
		if (Health > 0)
			return;
		OnDestroyWithScore.Invoke();
		DestroyWithoutScore();
	}
	public void DestroyWithoutScore()
	{
		lockOn.RemoveMe(transform);
		OnDestroyWithoutScore.Invoke();
		if (!Addressables.ReleaseInstance(this.gameObject))
			Destroy(gameObject);
	}
}