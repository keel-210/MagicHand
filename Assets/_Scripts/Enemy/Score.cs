using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public int score;
	Text text;
	void Start()
	{
		text = GetComponent<Text>();
	}
	void Update()
	{
		text.text = score.ToString("00000000");
	}
}