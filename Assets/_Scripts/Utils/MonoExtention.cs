using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

///


/// MonoBehaviorの拡張クラス
///
public static class MonoExtentsion
{
	/// 渡されたメソッドを指定時間後に実行する
	///
	public static IEnumerator DelayMethod(this MonoBehaviour mono, float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}