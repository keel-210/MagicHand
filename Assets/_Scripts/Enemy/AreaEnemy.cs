using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;
using System.Linq;
using System.Collections.Generic;

public class AreaEnemy : MonoBehaviour
{
	public List<EnemyMovement> EnemyList = new List<EnemyMovement>();
	public List<EnemyMovement> DeleteEnemyList = new List<EnemyMovement>();
	LockOn l;
	void Start()
	{
		l = FindObjectOfType<LockOn>();
		foreach (EnemyMovement e in EnemyList)
			e.Manager = transform.gameObject;
	}
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
					e.Bezier = op.Result;
					SummonEnemy(e);
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
	void SummonEnemy(EnemyMovement e)
	{
		Addressables.InstantiateAsync(e.EnemyReference).Completed += op =>
		{
			var enemys = op.Result.GetComponentsInChildren<IEnemy>();
			foreach (IEnemy enemy in enemys)
			{
				enemy.Initialize(e);
				enemy.lockOn = l;
			}
		};
	}
	[System.Serializable]
	public class EnemyMovement
	{
		public AssetReference EnemyReference, EnemyBezier;
		public AnimationCurve SpeedCurve;
		public GameObject Manager, Bezier;
		public Vector3 EnemyPos, EnemyRot, BezierPos, BezierRot;
		[Range(1, 30)] public int health;
		public int SpeedRatio;
		[Range(0, 180)] public float InitTime;
	}
}