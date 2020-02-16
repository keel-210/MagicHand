using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
	Score score;
	Gun LeftGun, RightGun;
	void Start()
	{
		score = FindObjectOfType<Score>();
	}
	public void AddScore()
	{
		score.GetComponent<RectTransform>().localScale = Vector3.one * 0.001f;
	}
}