using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public float hitDuration = 0.05f;
	public float hitWindowStart = -0.2f;
	public float hitWindowEnd = 0.1f;

	private BeatManager beatMgr;
	private GameManager gameMgr;

	private AudioClip sfx;
	private AudioSource sfxSource;

	public int lastResolvedBeat = int.MinValue;
	private bool[] beatArr;

	// Use this for initialization
	void Start () {
		beatMgr = FindObjectOfType(typeof(BeatManager)) as BeatManager;
		gameMgr = FindObjectOfType(typeof(GameManager)) as GameManager;
		sfxSource = gameObject.AddComponent<AudioSource>();
		sfxSource.loop = false;
		sfxSource.clip = sfx;
	}
	
	// Update is called once per frame
	void Update () {
		int curBeat = beatMgr.GetCurrentBeat();

		if (beatMgr.IsPlaying()) {
			CheckNewBeat(curBeat);
		}
		// TODO Logic for changing near/far sprites
	}

	private void CheckNewBeat(int curBeat) {
		float curTime = beatMgr.GetCurrentTime();

		if (curBeat < lastResolvedBeat) {
			// Already preempted
			//Debug.Log("Preempted" + " ; curBeat=" + curBeat + " ; lastResolvedBeat=" + lastResolvedBeat);
			return;
		}

		if (curBeat == lastResolvedBeat) {
			// Check for preempt
			if (curTime < beatMgr.GetTime(curBeat + 1) + hitWindowStart) {
				// Too early!
				if (IsHitting()) {
					DoSpam();
				}
				//Debug.Log("Preempted2" + " ; curBeat=" + curBeat + " ; lastResolvedBeat=" + lastResolvedBeat);
				return;
			}
			// Early but in window
			if (IsHitting()) {
				if (IsMyNote(curBeat + 1)) {
					DoHit();
					lastResolvedBeat = curBeat + 1;
				} else {
					DoSpam();
				}
			}
			//Debug.Log("Preempted3" + " ; curBeat=" + curBeat + " ; lastResolvedBeat=" + lastResolvedBeat);
			return;
		}

		// curBeat > lastResolvedBeat
		// Normal case
		if (curTime > beatMgr.GetTime(curBeat) + hitWindowEnd) {
			// Too late!
			if (IsMyNote(curBeat)) {
				DoMiss();
			}
			lastResolvedBeat = curBeat;
			//Debug.Log("Normal" + " ; curBeat=" + curBeat + " ; lastResolvedBeat=" + lastResolvedBeat);
			return;
		}

		// Not too late!
		if (IsHitting()) {
			if (IsMyNote(curBeat)) {
				DoHit();
				lastResolvedBeat = curBeat;
			} else {
				DoSpam();
			}
		}
		//Debug.Log("Late" + " ; curBeat=" + curBeat + " ; lastResolvedBeat=" + lastResolvedBeat);
	}

	private bool IsMyNote(int beat) {
		beatMgr.GetMeasure(beatMgr.GetTime(beat));
		return beatArr != null && beatArr[beatMgr.GetMeasureBeat(beat)] && beatMgr.GetMeasureType(beatMgr.GetMeasure(beatMgr.GetTime(beat))) == MeasureType.Perform;
	}

	private void DoHit() {
		gameMgr.AddHit();
		sfxSource.Play();
		Debug.Log("Hit!");
	}

	private void DoMiss() {
		// I missed a note!
		Debug.Log("Miss!");
	}

	private void DoSpam() {
		// I hit when I shouldn't have!
		Debug.Log("Spam!");
	}

	public bool IsHitting() {
		return Input.GetAxis("Strike") > 0f ||
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
			Input.GetKey("right");
	}

	public void SetSfx(AudioClip sfx) {
		this.sfx = sfx;
		sfxSource.Stop();
		sfxSource.clip = this.sfx;
	}

	public void SetBeatArr(bool[] arr) {
		this.beatArr = arr;
	}
}
