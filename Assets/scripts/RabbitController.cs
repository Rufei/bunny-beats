using UnityEngine;
using System.Collections;

public class RabbitController : MonoBehaviour {

	private SpriteRenderer sr;
	private BeatManager beatMgr;
	private GameManager gameMgr;
	private Animator animator;

	public Sprite readySprite;
	public Sprite beatSprite;
	public Sprite beatAltSprite;

	private SpriteRenderer spotlight;
	private SpriteRenderer carrot;

	public AudioClip sfx;
	private AudioSource sfxSource;

	public bool isFocused = false;
	public bool wasFocused = false;

	//public bool isReflected = false;

	private RabbitState state;
	private int lastResolvedBeat = int.MaxValue;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		state = GetComponent<RabbitState>();
		beatMgr = FindObjectOfType(typeof(BeatManager)) as BeatManager;
		gameMgr = FindObjectOfType(typeof(GameManager)) as GameManager;
		spotlight = transform.Find("Spotlight").GetComponent<SpriteRenderer>();
		spotlight.enabled = false;
		carrot = transform.Find("GraderCarrot").GetComponent<SpriteRenderer>();
		carrot.enabled = false;
		sfxSource = gameObject.AddComponent<AudioSource>();
		sfxSource.loop = false;
		sfxSource.volume = beatMgr.lowSfxVolume;
		sfxSource.clip = sfx;
	}
	
	// Update is called once per frame
	void Update () {
		int curBeat = beatMgr.GetCurrentBeat();

		if (beatMgr.IsPlaying()) {
			CheckNewBeat(curBeat);
		}
		// TODO Logic for changing near/far sprites

		lastResolvedBeat = curBeat;
	}

	void FixedUpdate() {
		CheckRest();
	}

	private void CheckNewBeat(int curBeat) {
		if (lastResolvedBeat != curBeat) {
			// New beat!
			int measureBeat = curBeat % (beatMgr.measureBeats);
			MeasureType mt = beatMgr.GetCurrentMeasureType();
			if (isFocused && mt == MeasureType.Perform) {
				SetFocus(false);
			}
			if (mt != MeasureType.None && mt != MeasureType.Taiko) {
				// Handle numbers
				if (IsHammerBeat(measureBeat)) {
					DoHammer(measureBeat);
				}
				// Handle visual
				DoAnim(measureBeat);
			}
		}
	}

	private bool IsHammerBeat(int measureBeat) {
		return state.beatArray[measureBeat];
	}

	private void DoHammer(int measureBeat) {
		/*
		if (state.isPlayerControlled) {
			sr.sprite = beatSprite;
		} else {
			sr.sprite = beatSprite;
		}
		*/
		//gameMgr.AddHit();
		DoSfx();
	}

	private void DoAnim(int measureBeat) {
		int animVal = state.animArray[measureBeat];
		if (animVal == 0) {
			sr.sprite = readySprite;
		} else if (animVal == 6) {
			sr.sprite = beatAltSprite;
		} else {
			sr.sprite = beatSprite;
		}
	}

	private void DoSfx() {
		sfxSource.Play();
	}

	private void CheckRest() {
		if (!beatMgr.IsPlaying()) {
			sr.sprite = readySprite;
		}
		if (sr.sprite != readySprite && lastResolvedBeat != beatMgr.GetCurrentBeat() && !IsHammerBeat(beatMgr.GetCurrentBeat() % (beatMgr.measureBeats))) {
			sr.sprite = readySprite;
		}
	}

	public void SetSfx(AudioClip sfx) {
		this.sfx = sfx;
	}

	public void SetFocus(bool isFocusing) {
		isFocused = isFocusing;
		if (isFocusing) {
			wasFocused = true;
		}
		sfxSource.volume = isFocused ? beatMgr.highSfxVolume : beatMgr.lowSfxVolume;
		spotlight.enabled = isFocused;
		carrot.enabled = isFocused;
		Debug.Log(transform.name + " was " + (isFocusing ? "" : "UN") + "FOCUSED");
	}

	/*
	public void SetReflected(bool isReflected) {
		this.isReflected = isReflected;
		transform.localRotation = Quaternion.Euler(0, isReflected ? 180 : 0, 0);
	}
	*/
}
