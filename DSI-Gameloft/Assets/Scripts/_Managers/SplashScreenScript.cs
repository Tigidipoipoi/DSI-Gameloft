using UnityEngine;
using System.Collections;

public class SplashScreenScript : MonoBehaviour {
    void Start() {
        PlayerPrefs.SetInt("RescuedCats", 0);
        Application.LoadLevel("StartMenu");
    }
}
