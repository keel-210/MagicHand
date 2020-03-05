using UnityEngine;

public class MusicalTimeChecker : MonoBehaviour
{
	public float checker;
	void Update()
	{
		checker = Music.MusicalTime;
		if (Music.IsJustChangedBeat())
			GetComponent<AudioSource>().Play();
	}
}