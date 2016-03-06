using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToCastScreen() {
		Application.LoadLevel("Cast");
	}

	public void ToMenuScreen() {
		Application.LoadLevel("Menu");
	}

	public void ToVideoScreen() {
		Application.LoadLevel("IntroScene");
	}

	public void ToGameScreen() {
		Application.LoadLevel("Game");
	}

	public void ToCreditsScreen() {
		Application.LoadLevel("Credits");
	}
}
