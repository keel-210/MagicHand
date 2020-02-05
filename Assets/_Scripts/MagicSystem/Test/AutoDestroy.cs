using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(this.DelayMethod(3f, () =>
		{
			Destroy(this.gameObject);
		}));
	}
}