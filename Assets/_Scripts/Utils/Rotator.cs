using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] public Vector3 RotationSpeed = default;
	void Update()
	{
		transform.rotation *= Quaternion.Euler(RotationSpeed * Time.deltaTime);
	}
}