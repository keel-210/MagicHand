using UnityEngine;
using System.Collections;
public class BulletSound : MonoBehaviour
{
	[SerializeField] AudioSource source, hatSource;
	[SerializeField] AudioClip hat;
	public AudioClip[] ChordProgression;
	public float[] waitTimes;
	[SerializeField] float waitTime = 0.1f;
	public void Initialize(int length)
	{
		transform.parent = null;
		StartCoroutine(PlayChord(length));
	}
	IEnumerator PlayChord(int l)
	{
		yield return new WaitForSeconds(waitTime);
		yield return new WaitUntil(Music.IsJustChangedBeat);
		hatSource.clip = hat;
		hatSource.Play();
		for (int i = 0; i < l; i++)
		{
			Chord(ChordProgression[i]);
			source.pitch = Mathf.Pow(1.05946309436f, i * 0.5f);
			yield return new WaitForSeconds(waitTimes[i]);
		}
		yield return new WaitForSeconds(waitTime + 0.1f);
		Destroy(gameObject);
	}
	void Chord(AudioClip clip)
	{
		source.clip = clip;
		source.Play();
	}
}