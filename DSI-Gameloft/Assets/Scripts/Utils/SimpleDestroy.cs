using UnityEngine;
using System.Collections;

public class SimpleDestroy : MonoBehaviour {

    public void DestroyNow()
    {
        Destroy(this.gameObject);
    }
}
