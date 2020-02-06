using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] Vector3 RotationSpeed;
	void Update()
	{
		transform.rotation *= Quaternion.Euler(RotationSpeed * Time.deltaTime);
	}
}