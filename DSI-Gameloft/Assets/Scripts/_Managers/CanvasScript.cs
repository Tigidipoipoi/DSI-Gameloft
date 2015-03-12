using UnityEngine;
using System.Collections;

public class CanvasScript : MonoBehaviour {
    void Awake () {
        DontDestroyOnLoad (this.gameObject);
    }
}
