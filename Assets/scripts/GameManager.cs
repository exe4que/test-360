using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class GameManager : MonoBehaviour {

    private void Awake() {
            VRSettings.enabled = Application.loadedLevelName.Equals("01_360view");
    }

    public void LoadLevel(int _level) {
        Application.LoadLevel(_level);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

}
