using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    PlayerScript m_PlayerScript;

    void Start () {
        m_PlayerScript = this.GetComponent<PlayerScript> ();
    }

    void Update () {
        if (Input.GetButtonDown ("Fire")) {
            Debug.Log ("Fire!");
            foreach (WeaponScript weapon in m_PlayerScript.m_Weapons) {
                if (weapon != null) {
                    weapon.Fire ();
                }
            }
        }
    }
}
