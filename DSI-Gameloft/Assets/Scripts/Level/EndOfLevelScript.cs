using UnityEngine;
using System.Collections;

public class EndOfLevelScript : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (FloorManager.instance.m_KeyAquired) {
                UIManager.instance.DisplayGameOverScreen(TimerManager.instance.m_RemainingTime <= 0.0f);
                //Application.LoadLevel("StartMenu");
            }
            else {
                Debug.Log("Gotta get the key !");
            }
        }
    }
}
