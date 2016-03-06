using UnityEngine;
using System.Collections;

public enum Difficulty { Easy, Medium, Hard };

public enum Action { Ready, Hit, AltHit };

public class RabbitState : MonoBehaviour {

	public bool[] beatArray;
	public int[] animArray;
	public Difficulty diff = Difficulty.Easy;

	public bool isPlayerControlled;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
