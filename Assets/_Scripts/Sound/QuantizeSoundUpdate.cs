using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class QuantizeSoundUpdate : MonoBehaviour
{
	public bool IsJustChangedBeatHalf, HalfChanged;
	List<float> BeatTime = new List<float>();
	float prevBeatTime;
	void Update()
	{
		if (Music.IsPlaying && Music.IsJustChangedBeat())
		{
			foreach (AudioSource s in Quantize.QuantizeQueue)
				if (s)
					s.Play();
			if (Quantize.QuantizeRowQueue.Count > 0 && Quantize.QuantizeRowQueue[0])
				Quantize.QuantizeRowQueue[0].Play();
			Quantize.QuantizeQueue.Clear();
			if (Quantize.QuantizeRowQueue.Count > 0)
				Quantize.QuantizeRowQueue.RemoveAt(0);

			BeatTime.Add(Time.time - prevBeatTime);
			if (BeatTime.Count > 10)
				BeatTime.RemoveAt(0);
			prevBeatTime = Time.time;
			HalfChanged = false;
		}
		if (Music.IsPlaying && Music.IsJustChangedBeat())
		{
			foreach (AudioSource s in Quantize.QuantizeHalfQueue)
				if (s)
					s.Play();
		}
		if (HalfChanged)
			IsJustChangedBeatHalf = false;
		if (!IsJustChangedBeatHalf && !HalfChanged && 0 < BeatTime.Count && Time.time > prevBeatTime + BeatTime.Average() / 2)
			IsJustChangedBeatHalf = HalfChanged = true;
		Quantize.IsJustChangedBeatHalf = IsJustChangedBeatHalf;
	}
}