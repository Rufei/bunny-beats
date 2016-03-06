using UnityEngine;
using System.Collections;

public class cineCamScript : MonoBehaviour {
	public SpriteRenderer frame1,frame2,frame3,frame4,frame5,white;
	float fadeRate = 2.0f;

	float moveTime;
	Vector3 target;
	Vector3 start;
	float timer;
	// Use this for initialization
	void Start () {
		frame2.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		frame2.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		frame3.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		frame4.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		frame5.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		white.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);

		moveTime = 5.0f;

		timer = 0.0f;
		target = new Vector3 (-118.0f, 253.0f, 0.0f);
		start = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 10.00f) {
			frame1.color = new Color (1.0f, 1.0f, 1.0f, frame1.color.a-fadeRate*Time.deltaTime);
		}

		if (timer > 10.00f && timer < 11.00f) {
			frame2.color = new Color (1.0f, 1.0f, 1.0f, frame2.color.a + fadeRate * Time.deltaTime);
		} else {
			frame2.color = new Color (1.0f, 1.0f, 1.0f, frame2.color.a - fadeRate * Time.deltaTime);
		}

		if (timer > 11.00f && timer < 13.500f) {
			frame3.color = new Color (1.0f, 1.0f, 1.0f, frame3.color.a + fadeRate * Time.deltaTime);
		} else {
			frame3.color = new Color (1.0f, 1.0f, 1.0f, frame3.color.a - fadeRate * Time.deltaTime);
		}

		if (timer > 13.50f && timer < 17.0f) {
			frame4.color = new Color (1.0f, 1.0f, 1.0f, frame4.color.a + fadeRate * Time.deltaTime);

		} else {
			frame4.color = new Color (1.0f, 1.0f, 1.0f, frame4.color.a - fadeRate * Time.deltaTime);
		}

		if (timer > 16.0f){
			frame5.color = new Color (1.0f, 1.0f, 1.0f, frame5.color.a + fadeRate * (0.25f) * Time.deltaTime);
		} //else {
		//	frame5.color = new Color (1.0f, 1.0f, 1.0f, frame5.color.a - fadeRate* (0.5f) * Time.deltaTime);
		//}

		float camStart = 19.00f;
		//float camEnd = 17.0f;

		if(timer > camStart ){
			Camera.main.orthographicSize -= 15.0f * Time.deltaTime;
			//Debug.Log(Vector3.LerpUnclamped(start,target,(timer-camStart)/camEnd).ToString());
			//Camera.main.transform.position = Vector3.Lerp(start,target,(timer - camEnd)/(camEnd-camStart));
		}

		if (timer > 20.0f) {
			white.color = new Color (1.0f, 1.0f, 1.0f, white.color.a + fadeRate * (0.65f) * Time.deltaTime);
		}

		frame2.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Max(0.0f,Mathf.Min(1.0f, frame2.color.a)));
		frame3.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Max(0.0f,Mathf.Min(1.0f, frame3.color.a)));
		frame4.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Max(0.0f,Mathf.Min(1.0f, frame4.color.a)));
		frame5.color = new Color (1.0f, 1.0f, 1.0f, Mathf.Max(0.0f,Mathf.Min(1.0f, frame5.color.a)));

		if (timer > 23.0f) {
			Application.LoadLevel("Game");
		}

	}

}
