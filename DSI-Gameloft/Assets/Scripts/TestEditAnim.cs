using UnityEngine;
using System.Collections;

public class TestEditAnim : MonoBehaviour {
    public IEnumerator Test(int squalala) {
        if (squalala < 1)
            Debug.Log("squalala");
        else
            Debug.Log("Et là ça fonctionne ?");

        yield return null;

        Debug.Log("Nous voila partis.");
    }
}
