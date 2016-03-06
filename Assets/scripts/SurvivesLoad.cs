using UnityEngine;
using System.Collections;

public class SurvivesLoad : MonoBehaviour {

	private AudioSource src;

	private static SurvivesLoad instance = null;
	public static SurvivesLoad Instance {
		get { return instance; }
	}

	void Start () {
		src = transform.Find("BgmSource").GetComponent<AudioSource>();
		src.Play();
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}

	public void TurnOff() {
		src.Stop();
		Destroy(this.gameObject);
	}
}
