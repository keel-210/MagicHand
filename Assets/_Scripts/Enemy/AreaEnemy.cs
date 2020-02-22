using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;
using System.Linq;
using System.Collections.Generic;

public class AreaEnemy : MonoBehaviour
{
	[SerializeField] AssetReference normalEnemy, multiEnemy;
	public List<EnemyMovement> EnemyList = new List<EnemyMovement>();
	public List<EnemyMovement> DeleteEnemyList = new List<EnemyMovement>();
	void Update()
	{
		foreach (EnemyMovement e in EnemyList)
		{
			if (e.InitTime < Time.time)
			{
				Addressables.InstantiateAsync(e.EnemyBezier).Completed += op =>
				{
					op.Result.transform.parent = transform;
					op.Result.transform.localPosition = e.BezierPos;
					op.Result.transform.rotation = Quaternion.Euler(e.BezierRot);
					SummonEnemy(e, op.Result);
				};
				DeleteEnemyList.Add(e);
			}
		}
		foreach (EnemyMovement de in DeleteEnemyList)
		{
			EnemyList.Remove(de);
		}
		DeleteEnemyList.Clear();
	}
	void SummonEnemy(EnemyMovement e, GameObject Bezier)
	{
		Addressables.InstantiateAsync(e.EnemyReference).Completed += op =>
		{
			op.Result.GetComponent<IEnemy>().Initialize(e.EnemyPos, transform);
			if (op.Result.GetComponent<MultiLockEnemy>())
				op.Result.GetComponent<MultiLockEnemy>().Health = e.health;
			op.Result.GetComponent<BezierSolution.BezierWalkerWithSpeed>().spline = Bezier.GetComponent<BezierSolution.BezierSpline>();
			op.Result.GetComponent<BezierSpeedChanger>().curve = e.SpeedCurve;
			op.Result.GetComponent<BezierSpeedChanger>().curveRatio = e.SpeedRatio;
		};
	}
	[System.Serializable]
	public class EnemyMovement
	{
		public AssetReference EnemyReference, EnemyBezier;
		public AnimationCurve SpeedCurve;
		public Vector3 EnemyPos, BezierPos, BezierRot;
		public int health, SpeedRatio;
		public float InitTime;
	}
}