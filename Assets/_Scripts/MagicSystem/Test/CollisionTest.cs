using UnityEngine;

public class CollisionTest : MonoBehaviour
{
	void OnCollisionEnter(Collision other)
	{
		Debug.Log("Hit");
	}
}