using UnityEngine;
using System.Collections;

public class SimpleDestroy : MonoBehaviour {

    void Start()
    {
        Destroy(this.gameObject,1.0f);
    }
}
