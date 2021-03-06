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
		ene.GetComponent<IEnemy>().score = score;
	}
	public void SetMultiEnemy(GameObject ene)
	{
		MultiLockEnemy.Add(ene);
		ene.GetComponent<IEnemy>().score = score;
	}

	public void RemoveEnemy(GameObject ene)
	{
		if (ene)
			Enemys.Remove(ene);
	}
	public void RemoveMultiEnemy(GameObject ene)
	{
		if (ene)
			MultiLockEnemy.Remove(ene);
	}
}