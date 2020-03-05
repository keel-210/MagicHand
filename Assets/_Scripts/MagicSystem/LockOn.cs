using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class LockOn : MonoBehaviour
{
	[SerializeField] public int LockOnLimit = default;
	[SerializeField] EnemyManagement enemy = default;
	[SerializeField] float LockOnDegreeThreshold = default, LockOnInterval = default;
	public List<Transform> LockedEnemys = new List<Transform>();
	public class LockOnEvent : UnityEvent<Vector3> { }
	public LockOnEvent onLockOnEvent = new LockOnEvent();
	public LockOnEvent onLockOnFailedEvent = new LockOnEvent();
	AudioSource audio;
	void Start()
	{
		StartCoroutine(Lock_Coroutine());
		audio = GetComponent<AudioSource>();
	}
	void MultiLock()
	{
		if (enemy.MultiLockEnemy.Count == 0)
			return;
		var enemysInSight = enemy.MultiLockEnemy
		.Where(x => Mathf.Cos(Mathf.PI * LockOnDegreeThreshold / 90) < Vector3.Dot(transform.forward, (x.transform.position - transform.position).normalized))
			.OrderByDescending(x => Mathf.Cos(Mathf.PI * LockOnDegreeThreshold / 90) < Vector3.Dot(transform.forward, (x.transform.position - transform.position).normalized));
		if (enemysInSight.Count() == 0)
			return;
		var e = enemysInSight.First().GetComponent<IEnemy>();
		if (LockedEnemys.Count < LockOnLimit && e.Health > 0)
		{
			LockedEnemys.Add(enemysInSight.First().transform);
			e.Health--;
			if (e.Health <= 0)
				e.KillSelf();
			e.lockOn = this;
			if (enemysInSight.Count() > 0)
				onLockOnEvent.Invoke(enemysInSight.First().transform.position);
			Quantize.QuantizePlay(audio);
		}
		else
		{
			onLockOnFailedEvent.Invoke(enemysInSight.First().transform.position);
		}
	}
	void EnemyLock()
	{
		if (enemy.Enemys.Count == 0)
			return;
		var enemysInSight = enemy.Enemys.
			Where(x => Mathf.Cos(Mathf.PI * LockOnDegreeThreshold / 90) < Vector3.Dot(transform.forward, (x.transform.position - transform.position).normalized));
		for (int i = 0; i < enemysInSight.Count(); i++)
		{
			GameObject LockTarget = enemysInSight.ElementAt(i);
			bool IsExistSameObjectInList = false;
			foreach (Transform t in LockedEnemys)
				IsExistSameObjectInList = IsExistSameObjectInList | t == LockTarget.transform;

			if (LockedEnemys.Count < LockOnLimit && !IsExistSameObjectInList && LockTarget.GetComponent<IEnemy>().Health > 0)
			{
				LockedEnemys.Add(LockTarget.transform);
				LockTarget.GetComponent<IEnemy>().Health--;
				LockTarget.GetComponent<IEnemy>().KillSelf();
				LockTarget.GetComponent<IEnemy>().lockOn = this;
				onLockOnEvent.Invoke(LockTarget.transform.position);
				Quantize.QuantizePlay(audio);
				break;
			}
			else
			{
				onLockOnFailedEvent.Invoke(LockTarget.transform.position);
			}
		}
	}
	public void RemoveMe(Transform t)
	{
		if (t)
			LockedEnemys.Remove(t);
	}
	public void CleanLockedEnemy() => LockedEnemys.Clear();
	IEnumerator Lock_Coroutine()
	{
		while (true)
		{
			MultiLock();
			EnemyLock();
			yield return new WaitUntil(Quantize.IsJustChangedBeatHalfFunc);
		}
	}
}