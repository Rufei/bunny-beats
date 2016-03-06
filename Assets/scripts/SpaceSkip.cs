using UnityEngine;
using System.Collections;

public class SpaceSkip : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")) {
			Application.LoadLevel("Game");
		}
	}
}
