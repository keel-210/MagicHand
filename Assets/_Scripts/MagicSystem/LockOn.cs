using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
public class LockOn : MonoBehaviour
{
	[SerializeField] int LockOnLimit = default;
	[SerializeField] EnemyManagement enemy = default;
	[SerializeField] float LockOnDegreeThreshold = default, LockOnInterval = default;
	public List<Transform> LockedEnemys = new List<Transform>();
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
			.Where(x => Mathf.Sin(Mathf.PI * LockOnDegreeThreshold / 90) > Vector3.Dot(transform.forward, (x.transform.position - transform.position).normalized))
					.OrderByDescending(x => Vector3.Dot(transform.forward, (x.transform.position - transform.position).normalized));
		if (LockedEnemys.Count < LockOnLimit && enemysInSight.First().GetComponent<IEnemy>().Health > 0)
		{
			LockedEnemys.Add(enemysInSight.First().transform);
			enemysInSight.First().GetComponent<IEnemy>().Health--;
			audio.Play();
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
				audio.Play();
				break;
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