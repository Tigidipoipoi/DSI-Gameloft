using UnityEngine;
using System.Collections;

public class PiercingBulletScript : BulletScript {
    public override void OnCollisionEnter (Collision other) {
        GameObject otherGO = other.gameObject;

        if (otherGO.layer != this.gameObject.layer) {
            if (this.gameObject.layer == m_EnemyBulletLayer) {
                if (otherGO.tag == "Player") {
                    TimerManager.instance.LoseTime (m_BulletStats.m_Power);
                    Debug.Log ("Player shot !");
                }
            }

            if (otherGO.layer == m_GroundLayer) {
                PreDestroy ();
            }
        }
    }
}
