using UnityEngine;
using System.Collections.Generic;
public class EnemyManagement : MonoBehaviour
{
	public List<GameObject> Enemys = new List<GameObject>();
	public List<GameObject> MultiLockEnemy = new List<GameObject>();
	Score score;
	void Start()
	{
		score = FindObjectOfType<Score>();
	}
	public void SetEnemy(GameObject ene)
	{
		Enemys.Add(ene);
	}
	public void SetMultiEnemy(GameObject ene)
	{
		MultiLockEnemy.Add(ene);
	}

	public void RemoveEnemy(GameObject ene)
	{
		Enemys.Remove(ene);
	}
	public void RemoveMultiEnemy(GameObject ene)
	{
		MultiLockEnemy.Remove(ene);
	}
}