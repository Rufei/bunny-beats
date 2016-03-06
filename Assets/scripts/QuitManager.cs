using UnityEngine;
using System.Collections;

public class QuitManager : MonoBehaviour {

    void Update () {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }
}