using UnityEngine;
using System.Collections;

public class CollectibleWeaponScript : MonoBehaviour {
    #region Members
    WeaponScript m_WeaponScript;
    PlayerScript m_PlayerScript;
    #endregion

    void Start() {
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        m_WeaponScript = this.transform.parent.GetComponent<WeaponScript>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            m_PlayerScript.LootWeapon(m_WeaponScript);
            Destroy(this.gameObject);
        }
    }
}
