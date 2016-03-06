using UnityEngine;
using System.Collections;

public class ReturnOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) ||
			Input.GetMouseButtonDown(1) ||
			Input.GetKeyDown("space") ||
			Input.GetKey("q") ||
			Input.GetKey("w") ||
			Input.GetKey("e") ||
			Input.GetKey("r") ||
			Input.GetKey("t") ||
			Input.GetKey("y") ||
			Input.GetKey("u") ||
			Input.GetKey("i") ||
			Input.GetKey("o") ||
			Input.GetKey("p") ||
			Input.GetKey("a") ||
			Input.GetKey("s") ||
			Input.GetKey("d") ||
			Input.GetKey("f") ||
			Input.GetKey("g") ||
			Input.GetKey("h") ||
			Input.GetKey("j") ||
			Input.GetKey("k") ||
			Input.GetKey("l") ||
			Input.GetKey("z") ||
			Input.GetKey("x") ||
			Input.GetKey("c") ||
			Input.GetKey("v") ||
			Input.GetKey("b") ||
			Input.GetKey("n") ||
			Input.GetKey("m") ||
			Input.GetKey("up") ||
			Input.GetKey("down") ||
			Input.GetKey("left") ||
			Input.GetKey("right")) {
			Application.LoadLevel("Menu");
		}
	}
}
