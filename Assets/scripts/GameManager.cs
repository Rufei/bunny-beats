using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RoundData { // Because Unity inspector hates enums
	public int numEasy;
	public int numMedium;
	public int numHard;
}

public class GameManager : MonoBehaviour {

	public int score;
	public bool isPaused;
	public int hitScore = 10;
	public int maxScore;
	public RoundData[] rounds = new RoundData[3];

	public Transform[] rabbitLocs = new Transform[4];

	public Text scoreText;
	public Image learnImg;
	public Image performImg;
	public SpriteRenderer mochi;

	public AudioClip clipGradeS;
	public AudioClip clipGradeA;
	public AudioClip clipGradeB;
	public AudioClip clipGradeF;

	private AudioSource gradeSource;
	private BeatManager beatMgr;
	private PlayerManager playerMgr;
	private RabbitFactory rabbitFactory;
	private int currentRound = 0;
	private int lastResolvedMeasure = int.MinValue;

	private float heartbeat;
	public bool isHeartbeat = false;

	// Use this for initialization
	void Start () {
		beatMgr = FindObjectOfType(typeof(BeatManager)) as BeatManager;
		playerMgr = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
		rabbitFactory = FindObjectOfType(typeof(RabbitFactory)) as RabbitFactory;
		gradeSource = gameObject.AddComponent<AudioSource>();
		gradeSource.loop = false;
		gradeSource.clip = clipGradeF;
		mochi.enabled = false;
		score = 0;
		scoreText.enabled = false;
		learnImg.enabled = false;
		performImg.enabled = false;
		heartbeat = Time.time;
		DoIntro();
	}
	
	// Update is called once per frame
	void Update () {
		if (!beatMgr.IsPlaying()) {
			if (currentRound > 0) {
				CheckPlayStart();
			}
		} else {
			MeasureType curType = beatMgr.GetCurrentMeasureType();
			if (curType == MeasureType.Taiko) {
				DoScoring(true);
			} else {
				DoScoring(false);
			}
			if (curType == MeasureType.Learn) {
				learnImg.enabled = true;
			} else {
				learnImg.enabled = false;
			}
			if (curType == MeasureType.Perform) {
				performImg.enabled = true;
			} else {
				performImg.enabled = false;
			}
			if ((int) (heartbeat % beatMgr.GetBeatReleaseTime()) == 0) {
				DoHeartbeat();
			}
		}

		lastResolvedMeasure = beatMgr.GetCurrentMeasure();
	}

	private void DoHeartbeat() {
		isHeartbeat = !isHeartbeat;
	}

	private void DoIntro() {

	}

	private void DoScoring(bool isEnabled) {
		if (scoreText.enabled != isEnabled) {
			scoreText.enabled = isEnabled;
			if (isEnabled) {
				float percentage = ((float)score) / ((float) maxScore);
				if (percentage == 1f) {
					gradeSource.clip = clipGradeS;
				} else if (percentage > .7f) {
					gradeSource.clip = clipGradeA;
				} else if (percentage > .5f) {
					gradeSource.clip = clipGradeB;
				} else {
					gradeSource.clip = clipGradeF;
				}
				gradeSource.Play();
				mochi.enabled = false;
			}
		}
	}

	private void CheckPlayStart() {
		if (!beatMgr.IsPlaying()) {
			if (currentRound > rounds.Length) {
				currentRound = 0;
			}
			StartRound(currentRound);
		}
	}

	public void StartRoundManual() {
		if (!beatMgr.IsPlaying()) {
			if (currentRound > rounds.Length) {
				currentRound = 0;
			}
			StartRound(currentRound);
		}
	}

	private void StartRound(int roundNum) {
		SetupRabbits(roundNum);
		playerMgr.lastResolvedBeat = int.MinValue;
		beatMgr.lastResolvedMeasure = int.MinValue;
		beatMgr.StartBeat();
		mochi.enabled = true;
		currentRound += 1;
	}

	private void SetupRabbits(int roundNum) {
		if (roundNum < 0 || roundNum >= rounds.Length) {
			return;
		}

		List<Difficulty> remainingDifficulties = new List<Difficulty>(4);
		RoundData rd = rounds[roundNum];
		for (int i = 0; i < rd.numEasy; i++) {
			remainingDifficulties.Add(Difficulty.Easy);
		}
		for (int i = 0; i < rd.numMedium; i++) {
			remainingDifficulties.Add(Difficulty.Medium);
		}
		for (int i = 0; i < rd.numHard; i++) {
			remainingDifficulties.Add(Difficulty.Hard);
		}
		foreach (Transform loc in rabbitLocs) {
			Difficulty diff = Utils.GetRand(remainingDifficulties);
			Transform rabbit = SetRabbitAt(loc, diff, loc.GetComponent<LocationState>().isClose, true);
			if (rabbit != null) {
				RabbitState state = rabbit.GetComponent<RabbitState>();
				foreach (bool b in state.beatArray) {
					if (b) {
						maxScore += hitScore * 2;
					}
				}
			}
			remainingDifficulties.Remove(diff);
		}
	}

	private Transform GetRabbitAt(Transform location) {
		if (location.childCount == 0) {
			return null;
		}
		return location.GetChild(0);
	}

	private Transform SetRabbitAt(Transform location, Difficulty diff, bool isNear, bool isOverwrite = false) {
		Transform oldRabbit = GetRabbitAt(location);
		if (oldRabbit != null) {
			if (!isOverwrite) {
				return null;
			} else {
				oldRabbit.parent = null;
				Destroy(oldRabbit.gameObject);
			}
		}
		Transform rabbit = rabbitFactory.CreateRabbit(diff, isNear);
		rabbit.position = location.position;
		rabbit.rotation = location.rotation;
		rabbit.parent = location;
		return rabbit;
	}

	public void AddHit() {
		score += hitScore;
	}

	public Transform GetFreshRabbit() {
		List<Transform> freshRabbits = new List<Transform>(4);
		foreach (Transform loc in rabbitLocs) {
			Transform rabbit = GetRabbitAt(loc);
			if (rabbit != null) {
				RabbitController ctrl = rabbit.GetComponent<RabbitController>();
				if (!ctrl.wasFocused) {
					freshRabbits.Add(rabbit);
				}
			}
		}
		return Utils.GetRand(freshRabbits);
	}

	public void SetNewFocus() {
		Transform rabbit = GetFreshRabbit();
		if (rabbit == null) {
			Debug.Log("I have no rabbit and I must scream.");
		} else {
			RabbitController ctrl = rabbit.GetComponent<RabbitController>();
			ctrl.SetFocus(true);
			playerMgr.SetSfx(ctrl.sfx);
			RabbitState state = ctrl.GetComponent<RabbitState>();
			playerMgr.SetBeatArr(state.beatArray);
		}
	}
}
