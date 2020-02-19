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
			if (e.InitTime > Time.time)
			{
				Addressables.InstantiateAsync(e.EnemyBezier).Completed += op =>
				{
					op.Result.transform.position = e.BezierPos;
					op.Result.transform.rotation = Quaternion.Euler(e.BezierRot);
					SummonEnemy(e.EnemyReference, op.Result, e.EnemyPos, e.health);
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
	void SummonEnemy(AssetReference reference, GameObject Bezier, Vector3 pos, int health)
	{
		if (reference == normalEnemy)
			Normal(pos, Bezier);
		if (reference == multiEnemy)
			Multi(pos, health, Bezier);
	}
	void Normal(Vector3 pos, GameObject Bezier)
	{
		Addressables.InstantiateAsync(normalEnemy).Completed += op =>
		{
			op.Result.GetComponent<IEnemy>().Initialize(pos, transform);
			op.Result.GetComponent<BezierSolution.BezierWalkerWithSpeed>().spline = Bezier.GetComponent<BezierSolution.BezierSpline>();
		};
	}
	void Multi(Vector3 pos, int Health, GameObject Bezier)
	{
		Addressables.InstantiateAsync(multiEnemy).Completed += op =>
		{
			op.Result.GetComponent<IEnemy>().Initialize(pos, transform);
			op.Result.GetComponent<MultiLockEnemy>().Health = Health;
			op.Result.GetComponent<BezierSolution.BezierWalkerWithSpeed>().spline = Bezier.GetComponent<BezierSolution.BezierSpline>();
		};
	}
	[System.Serializable]
	public class EnemyMovement
	{
		public AssetReference EnemyReference, EnemyBezier;
		public Vector3 EnemyPos, BezierPos, BezierRot;
		public int health;
		public float InitTime;
	}
}