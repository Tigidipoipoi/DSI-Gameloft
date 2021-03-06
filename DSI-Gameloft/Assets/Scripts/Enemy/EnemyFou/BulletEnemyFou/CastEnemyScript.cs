﻿using UnityEngine;
using System.Collections;

public class CastEnemyScript : MonoBehaviour {
    #region Members
    public float m_CastDelay;
    public float m_CastDamage;
    public float m_CastDisapear;
    private bool m_Laser;
    #endregion

    void Start() {
        m_Laser = false;
        StartCoroutine(Cast());
    }

    IEnumerator Cast() {
        yield return new WaitForSeconds(m_CastDelay);
        m_Laser = true;
        Debug.Log("laser");

    }
    void Burn(GameObject other) {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(m_CastDisapear);
        Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider other) {
        if (m_Laser == true) {
            m_Laser = false;
            if (other.tag == "Player") {
                Debug.Log("touch_player");
                Burn(other.gameObject);
            }
            else {
                StartCoroutine(Destroy());
            }
        }

    }
}
