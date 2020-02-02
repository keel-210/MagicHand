using UnityEngine;
using UnityEngine.UI;
public class HandSign_BendViewer : MonoBehaviour
{
	[SerializeField] Text text;
	HandSignDetector detector;
	void Start()
	{
		detector = GetComponent<HandSignDetector>();
	}
	void Update()
	{
		text.text = System.Enum.GetName(typeof(HandSign_Bend), detector.sign);
	}
}