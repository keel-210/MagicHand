using UnityEngine;
using UnityEngine.UI;
public class GunDotViewer : MonoBehaviour
{
	[SerializeField] Slider slider;
	Gun gun;
	void Start()
	{
		gun = GetComponent<Gun>();
	}
	void Update()
	{
		slider.value = gun.DirDot;
	}
}