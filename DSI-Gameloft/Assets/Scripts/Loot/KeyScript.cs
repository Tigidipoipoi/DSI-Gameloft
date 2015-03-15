using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            FloorManager.instance.GetKey();
            Destroy (this.gameObject);
        }
    }
}
