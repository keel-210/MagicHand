using UnityEngine;

public class BulletSoundTest : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(this.DelayMethod(1f, () =>
		{
			GetComponent<BulletSound>().Initialize(8);
		}));
	}
}