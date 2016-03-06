using UnityEngine;
using System.Collections;

public class RemovesSurvivable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (SurvivesLoad.Instance != null) {
			SurvivesLoad.Instance.TurnOff();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
