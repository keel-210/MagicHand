using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class LockOn : MonoBehaviour
{
	[SerializeField] int LockOnLimit = default;
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
	void Lock()
	{
		MultiLock();
		EnemyLock();
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
		if (LockedEnemys.Count < LockOnLimit && enemysInSight.First().GetComponent<IEnemy>().Health > 0)
		{
			LockedEnemys.Add(enemysInSight.First().transform);
			enemysInSight.First().GetComponent<IEnemy>().Health--;
			onLockOnEvent.Invoke(enemysInSight.First().transform.position);
			audio.Play();
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
				onLockOnEvent.Invoke(LockTarget.transform.position);
				audio.Play();
				break;
			}
			else
			{
				onLockOnFailedEvent.Invoke(LockTarget.transform.position);
			}
		}
	}
	public void CleanLockedEnemy() => LockedEnemys.Clear();
	IEnumerator Lock_Coroutine()
	{
		while (true)
		{
			Lock();
			yield return new WaitForSeconds(LockOnInterval);
		}
	}
}