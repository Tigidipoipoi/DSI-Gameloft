using UnityEngine;
using System.Collections;

public class ConsumablesButtonScript : MonoBehaviour {

    public void DestroyNow(string consumable)
    {
        if (consumable == "freeze")
        {
            ConsumablesManager.instance.StartCoroutine("LetsFreeze");
        }

        if (consumable == "death")
        {
            ConsumablesManager.instance.StartCoroutine("LetsKill");
        }

        Destroy(this.gameObject);
    }
}
