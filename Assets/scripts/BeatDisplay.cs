using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeatDisplay : MonoBehaviour {

	private BeatManager beatMgr;

	private Text displayText;
	private string startText = "";

	// Use this for initialization
	void Start () {
		beatMgr = FindObjectOfType(typeof(BeatManager)) as BeatManager;
		displayText = GetComponent<Text>();
		startText = displayText.text;
	}
	
	// Update is called once per frame
	void Update () {
		displayText.text = startText + beatMgr.GetCurrentBeat();
	}
}
