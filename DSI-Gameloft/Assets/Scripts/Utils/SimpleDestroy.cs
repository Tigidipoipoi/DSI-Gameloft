using UnityEngine;
using System.Collections;

public class SimpleDestroy : MonoBehaviour {

    void DestroyNow()
    {
        Destroy(this.gameObject);
    }
}
