using UnityEngine;
using System.Collections.Generic;
public static class Quantize
{
	public static bool IsJustChangedBeatHalf;
	public static bool IsJustChangedBeatHalfFunc() { return IsJustChangedBeatHalf; }

	public static List<AudioSource> QuantizeQueue = new List<AudioSource>();
	public static List<AudioSource> QuantizeHalfQueue = new List<AudioSource>();
	public static List<AudioSource> QuantizeRowQueue = new List<AudioSource>();
	public static void QuantizePlay(AudioSource source)
	{
		QuantizeQueue.Add(source);
	}
	public static void QuantizeHalfPlay(AudioSource source)
	{
		QuantizeHalfQueue.Add(source);
	}
	public static void QuantizeRowPlay(AudioSource source)
	{
		QuantizeRowQueue.Add(source);
	}
}