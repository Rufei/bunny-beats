using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AxisDisplay : MonoBehaviour {

	private Text displayText;
	private string startText = "";

	// Use this for initialization
	void Start () {
		displayText = GetComponent<Text>();
		startText = displayText.text;
	}
	
	// Update is called once per frame
	void Update () {
		displayText.text = startText + Input.GetAxis("Strike");
	}
}
