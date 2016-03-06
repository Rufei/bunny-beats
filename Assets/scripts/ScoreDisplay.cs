using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	private GameManager gameMgr;

	private Text displayText;
	private string startText = "";

	// Use this for initialization
	void Start () {
		gameMgr = FindObjectOfType(typeof(GameManager)) as GameManager;
		displayText = GetComponent<Text>();
		startText = displayText.text;
	}
	
	// Update is called once per frame
	void Update () {
		displayText.text = startText + gameMgr.score + " of " + gameMgr.maxScore;
	}
}
