using UnityEngine;
using System.Collections;
using System;

public enum MeasureType { Lead, Learn, Perform, Coda, Taiko, None };

public class BeatManager : MonoBehaviour {

	public AudioClip bgmClip;
	public AudioSource bgmSource;

	public int bpm = 480;
	public int measureBeats = 32;
	public int leadMeasures = 2;
	public int codaMeasures = 2;
	public int taikoMeasures = 2;

	public int learnMeasuresPerPhrase = 2;
	public int performMeasuresPerPhrase = 2;
	public int normalPhrasesPerRound = 4;

	public float lowSfxVolume = 0.2f;
	public float highSfxVolume = 1f;

	private GameManager gameMgr;

	private float bps;
	private float spb;
	private float beatReleaseTime;
	private int totalMeasures;
	public int lastResolvedMeasure = int.MinValue;

	// Use this for initialization
	void Start () {
		gameMgr = FindObjectOfType(typeof(GameManager)) as GameManager;
		bgmSource.clip = bgmClip;
		bgmSource.loop = false;

		bps = bpm / 60f;
		spb = bps != 0 ? 1/bps : 0;
		beatReleaseTime = spb;
		totalMeasures = leadMeasures + ((learnMeasuresPerPhrase + performMeasuresPerPhrase) * normalPhrasesPerRound) + codaMeasures + taikoMeasures;
	}
	
	// Update is called once per frame
	void Update () {
		int curMeasure = GetCurrentMeasure();

		if (lastResolvedMeasure < curMeasure) {
			if (GetCurrentMeasureType() == MeasureType.Learn) {
				gameMgr.SetNewFocus();
				lastResolvedMeasure = curMeasure + 1;
			} else {
				lastResolvedMeasure = curMeasure;
			}
		}
	}

	// Starts the game with lead measures
	public void StartBeat() {
		bgmSource.time = 0f;
		bgmSource.Play();
		lastResolvedMeasure = int.MinValue;
	}

	public float GetCurrentTime() {
		return bgmSource.time;
	}

	public int GetCurrentBeat() {
		return GetBeat(GetCurrentTime());
	}

	public int GetCurrentMeasure() {
		return GetMeasure(GetCurrentTime());
	}

	public MeasureType GetCurrentMeasureType() {
		return GetMeasureType(GetCurrentMeasure());
	}

	public int GetBeat(float time) {
		return (int) Math.Floor(time * bps);
	}

	public int GetMeasure(float time) {
		return (int) Math.Floor(time * bps / ((float)measureBeats));
	}

	public int GetMeasureBeat(int beat) {
		return beat % measureBeats;
	}

	public MeasureType GetMeasureType(int measure) {
		int max = totalMeasures - 2; // Position, not size
		int cur = measure;

		if (cur > max || cur < 0) {
			return MeasureType.None;
		}
		cur -= leadMeasures;
		if (cur < 0) {
			return MeasureType.Lead;
		}

		max -= taikoMeasures;
		if (cur >= max) {
			return MeasureType.Taiko;
		}
		max -= codaMeasures;
		if (cur >= max) {
			return MeasureType.Coda;
		}

		if (cur % (learnMeasuresPerPhrase + performMeasuresPerPhrase) < learnMeasuresPerPhrase) {
			return MeasureType.Learn;
		} else {
			return MeasureType.Perform;
		}
	}

	public float GetTime(int beat) {
		return spb * beat;
	}

	public float GetBeatReleaseTime() {
		return beatReleaseTime;
	}

	public bool IsPlaying() {
		return bgmSource.isPlaying;
	}
}
