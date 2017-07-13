using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class GameManager : MonoBehaviour {

    private void Awake() {
        if (Application.loadedLevelName.Equals("01_360view")) {
            StartCoroutine(LoadDevice("cardboard"));
        }
    }

    public void LoadLevel(int _level) {
        Application.LoadLevel(_level);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Application.loadedLevelName.Equals("00_menu")) {
                Application.Quit();
            } else {
                if (Application.loadedLevelName.Equals("01_360view")) {
                    StartCoroutine(LoadDevice("none"));
                }
                Application.LoadLevel("00_menu");
            }
            
        }
    }

    public IEnumerator LoadDevice(string _newDevice) {
        VRSettings.LoadDeviceByName(_newDevice);
        yield return null;
        VRSettings.enabled = _newDevice.Equals("cardboard");
    }

}
