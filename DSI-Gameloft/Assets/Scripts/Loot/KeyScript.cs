using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            FloorManager.instance.m_KeyAquired = true;
            Destroy (this.gameObject);
        }
    }
}
