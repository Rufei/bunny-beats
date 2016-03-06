using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Utils : MonoBehaviour {

	public static string toString(Array arr) {
		string str = "[";
		foreach (System.Object o in arr) {
			str += o + ",";
		}
		str += "]";
		return str;
	}

	public static T GetRand<T>(List<T> list) {
		if (list.Count == 0) {
			return default (T);
		}
		int pos = (int)(UnityEngine.Random.Range(0, list.Count));
		pos = pos == list.Count ? list.Count - 1 : pos; // Exclude the max
		return list[pos];
	}

	public static T GetRand<T>(T[] arr) {
		if (arr.Length == 0) {
			return default (T);
		}
		int pos = (int)(UnityEngine.Random.Range(0, arr.Length));
		pos = pos == arr.Length ? arr.Length - 1 : pos; // Exclude the max
		return arr[pos];
	}
}
