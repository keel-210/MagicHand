using UnityEngine;
using UnityEngine.UI;
public class LockOnCount : MonoBehaviour
{
	[SerializeField] LockOn lockOn = default;
	Text text;
	void Start()
	{
		text = GetComponent<Text>();
		if (text == null)
			this.enabled = false;
	}
	void Update()
	{
		text.text = lockOn.LockedEnemys.Count.ToString();
	}
}