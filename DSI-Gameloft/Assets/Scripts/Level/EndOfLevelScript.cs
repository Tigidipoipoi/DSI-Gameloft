using UnityEngine;
using System.Collections;

public class EndOfLevelScript : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (FloorManager.instance.m_KeyAquired) {
                // End of level
                Debug.Log("End of level");
            }
            else {
                Debug.Log("Gotta get the key !");
            }
        }
    }
}
