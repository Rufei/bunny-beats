using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Sprites {
	public Sprite nearReady;
	public Sprite farReady;
	public Sprite nearHit;
	public Sprite farHit;
	public Sprite nearAltHit;
	public Sprite farAltHit;

	public Sprite GetSprite(Action action, bool isNear) {
		switch (action) {
			case Action.Ready:
				return isNear ? nearReady : farReady;
			case Action.Hit:
				return isNear ? nearHit : farHit;
			case Action.AltHit:
				return isNear ? nearAltHit : farAltHit;
			default:
				return null;
		}
	}
}

public class BeatAnimPair {
	public bool[] beats;
	public int[] anims;
	public AudioClip sfx;
	public BeatAnimPair(int[] beatArray, int[] animArray, string sfxName) {
		bool[] bools = new bool[beatArray.Length];
		for (int i = 0; i < beatArray.Length; i++) {
			bools[i] = beatArray[i] == 1 ? true : false;
		}
		beats = bools;
		anims = animArray;
		sfx = Resources.Load(sfxName) as AudioClip;
	}
}

public class RabbitFactory : MonoBehaviour {

	// Prefabs
	public Transform rabbitPrefab;

	public Transform spawnPoint;
	// ORDERING IS IMPORTANT
	public Sprites easySprites = new Sprites();
	public Sprites mediumSprites = new Sprites();
	public Sprites hardSprites = new Sprites();

	private List<BeatAnimPair> easyBeats;
	private List<BeatAnimPair> mediumBeats;
	private List<BeatAnimPair> hardBeats;

	private List<BeatAnimPair> lastFourBeats;

	private int rabbitNum = 0;

	// Use this for initialization
	void Start () {
		easyBeats = new List<BeatAnimPair>(6);
		// 1
		easyBeats.Add(new BeatAnimPair(
			new int[]{1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0},
			new int[]{4,4,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0,0,4,4,0,0,4,4,0,0,0,0,0,0},
			"sound/x001"));
		// 2
		easyBeats.Add(new BeatAnimPair(
			new int[]{0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0},
			new int[]{0,0,4,4,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,0,0,0,0},
			"sound/x002"));
		// 3
		easyBeats.Add(new BeatAnimPair(
			new int[]{0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0},
			new int[]{0,0,0,0,4,4,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,0,0},
			"sound/x003"));
		// 4
		easyBeats.Add(new BeatAnimPair(
			new int[]{1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0},
			new int[]{4,4,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,0,0,4,4,0,0},
			"sound/x004"));
		// 5
		easyBeats.Add(new BeatAnimPair(
			new int[]{0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0},
			new int[]{0,0,0,0,4,4,0,0,4,4,0,0,0,0,0,0,4,4,0,0,0,0,4,4,0,0,0,0,4,4,0,0},
			"sound/x005"));
		// 6
		easyBeats.Add(new BeatAnimPair(
			new int[]{1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0},
			new int[]{4,4,0,0,0,0,4,4,0,0,0,0,4,4,0,0,0,0,0,0,4,4,0,0,0,0,4,4,0,0,0,0},
			"sound/x006"));

		mediumBeats = new List<BeatAnimPair>(6);
		// 7
		mediumBeats.Add(new BeatAnimPair(
			new int[]{0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0},
			new int[]{0,0,4,4,0,0,4,4,0,0,4,4,0,0,0,0,0,0,4,4,0,0,4,4,0,0,4,4,0,0,4,4},
			"sound/x007"));
		// 8
		mediumBeats.Add(new BeatAnimPair(
			new int[]{0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0},
			new int[]{0,0,0,0,4,0,4,0,0,0,0,0,4,0,4,0,0,0,0,0,4,0,4,0,0,0,0,0,4,0,4,0},
			"sound/x008"));
		// 9
		mediumBeats.Add(new BeatAnimPair(
			new int[]{1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0},
			new int[]{4,4,0,0,4,4,0,0,0,0,4,4,0,0,4,4,0,0,4,4,0,0,4,0,4,0,0,0,4,4,0,0},
			"sound/x009"));
		// 10
		mediumBeats.Add(new BeatAnimPair(
			new int[]{0,0,0,0,0,0,0,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,1,0},
			new int[]{0,0,0,0,0,0,0,0,4,0,4,0,4,0,4,0,0,0,0,0,0,0,0,0,4,0,4,0,0,0,4,4},
			"sound/x010"));
		// 11
		mediumBeats.Add(new BeatAnimPair(
			new int[]{0,0,0,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0},
			new int[]{0,0,0,0,4,0,4,0,0,0,4,0,4,0,0,0,4,4,0,0,0,0,0,0,4,0,4,0,4,0,0,0},
			"sound/x011"));
		// 12
		mediumBeats.Add(new BeatAnimPair(
			new int[]{1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0},
			new int[]{4,4,0,0,4,0,4,0,4,4,0,0,4,4,0,0,4,4,0,0,4,4,0,0,4,4,0,0,4,0,4,0},
			"sound/x012"));

		hardBeats = new List<BeatAnimPair>(6);
		// 13
		hardBeats.Add(new BeatAnimPair(
			new int[]{1,0,0,1,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,1,0,0,1,0,0,0,1,0,1,0,1,0},
			new int[]{5,5,0,6,6,0,5,5,0,0,5,5,6,6,0,0,5,5,0,6,6,0,5,5,0,0,6,6,5,5,6,6},
			"sound/x013"));
		// 14
		hardBeats.Add(new BeatAnimPair(
			new int[]{0,0,1,0,1,0,0,0,1,0,0,1,0,0,1,0,0,0,1,0,1,0,0,0,1,0,0,1,1,0,0,1},
			new int[]{0,0,5,5,6,6,0,0,5,5,0,6,6,0,5,5,0,0,6,6,5,5,0,0,6,6,0,5,5,0,0,6},
			"sound/x014"));
		// 15
		hardBeats.Add(new BeatAnimPair(
			new int[]{1,1,0,1,0,1,0,1,1,0,1,0,1,0,0,0,1,1,0,1,0,1,0,1,1,0,1,0,0,0,0,0},
			new int[]{5,6,6,5,5,6,6,5,6,6,5,5,6,6,0,0,6,5,5,6,6,5,5,6,5,5,6,6,0,0,0,0},
			"sound/x015"));
		// 16
		hardBeats.Add(new BeatAnimPair(
			new int[]{0,0,1,0,1,0,1,1,0,0,1,0,1,0,0,1,0,0,1,0,1,0,1,1,0,0,1,0,1,1,1,0},
			new int[]{0,0,5,5,6,6,5,6,6,0,5,5,6,6,0,5,5,0,5,5,6,6,5,6,6,0,5,5,6,5,6,6},
			"sound/x016"));
		// 17
		hardBeats.Add(new BeatAnimPair(
			new int[]{1,0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,1,0,0,0},
			new int[]{5,5,6,6,0,5,5,6,6,5,5,6,5,5,6,6,5,5,6,6,5,6,6,5,5,6,6,5,6,6,0,0},
			"sound/x017"));
		// 18
		hardBeats.Add(new BeatAnimPair(
			new int[]{1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1,0,0,0},
			new int[]{5,6,6,5,6,6,5,6,6,5,6,6,5,6,6,5,6,6,5,6,6,5,6,6,5,6,6,5,6,6,0,0},
			"sound/x018"));

		lastFourBeats = new List<BeatAnimPair>(4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//public Transform CreateRabbit(Difficulty diff, bool isNear, bool isReflected) {
	public Transform CreateRabbit(Difficulty diff, bool isNear) {
		List<BeatAnimPair> baList = null;
		Sprites sprites = null;
		switch (diff) {
			case Difficulty.Easy:
				baList = easyBeats;
				sprites = easySprites;
				break;
			case Difficulty.Medium:
				baList = mediumBeats;
				sprites = mediumSprites;
				break;
			case Difficulty.Hard:
				baList = hardBeats;
				sprites = hardSprites;
				break;
			default:
				baList = easyBeats;
				sprites = easySprites;
				break;
		}
		int pos = (int)(Random.Range(0f, 6f));
		pos = pos == 6 ? 5 : pos; // Exclude the max
		BeatAnimPair data = Utils.GetRand(baList);
		while (lastFourBeats.Contains(data)) {
			data = Utils.GetRand(baList);
		}

		Transform rabbit = Instantiate(rabbitPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as Transform;
		rabbit.name = diff.ToString() + "Rabbit-id" + rabbitNum;
		//rabbit.SetReflected(isReflected);

		// Set sprites
		SpriteRenderer sr = rabbit.GetComponent<SpriteRenderer>();
		RabbitController ctrl = rabbit.GetComponent<RabbitController>();

		ctrl.readySprite = sprites.GetSprite(Action.Ready, isNear);
		ctrl.beatSprite = sprites.GetSprite(Action.Hit, isNear);
		ctrl.beatAltSprite = sprites.GetSprite(Action.AltHit, isNear);
		sr.sprite = ctrl.readySprite;
		
		// Set model
		RabbitState state = rabbit.GetComponent<RabbitState>();
		state.beatArray = data.beats;
		state.animArray = data.anims;

		ctrl.SetSfx(data.sfx);

		rabbitNum += 1;
		if (lastFourBeats.Count == 4) {
			for (int i = 2; i >= 0; i--) {
				lastFourBeats[i] = lastFourBeats[i+1];
			}
			lastFourBeats[3] = data;
		} else {
			lastFourBeats.Add(data);
		}

		return rabbit;
	}

	public Sprite GetSprite(Difficulty diff, Action action, bool isNear) {
		switch (diff) {
			case Difficulty.Easy:
				return easySprites.GetSprite(action, isNear);
			case Difficulty.Medium:
				return mediumSprites.GetSprite(action, isNear);
			case Difficulty.Hard:
				return hardSprites.GetSprite(action, isNear);
			default:
				return easySprites.GetSprite(action, isNear);
		}
	}
}
