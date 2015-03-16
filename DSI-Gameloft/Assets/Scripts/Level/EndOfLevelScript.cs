using UnityEngine;
using System.Collections;

public class EndOfLevelScript : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (FloorManager.instance.m_KeyAquired) {
                // End of level
                Debug.Log("End of level");
                if (TimerManager.instance.m_RemainingTime > 0.0f) {
                    //int rescuedCat = PlayerPrefs.GetInt("RescuedCats", 0);
                    PlayerPrefs.SetInt("RescuedCats", 1);//rescuedCat + 1);
                }
                Application.LoadLevel("StartMenu");
            }
            else {
                Debug.Log("Gotta get the key !");
            }
        }
    }
}
