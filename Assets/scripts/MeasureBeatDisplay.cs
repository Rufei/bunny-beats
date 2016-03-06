using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeasureBeatDisplay : MonoBehaviour {

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
		int measureBeat = beatMgr.GetCurrentBeat() % beatMgr.measureBeats;
		displayText.text = startText + measureBeat + " (" + beatMgr.GetCurrentBeat() + " % " + beatMgr.measureBeats + ")";
	}
}
