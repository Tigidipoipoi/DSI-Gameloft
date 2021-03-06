﻿using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {
    #region Members
    public int m_MaxHP = 3;

    Obstacle m_ObstacleStats;
    #endregion

    void Start () {
        m_ObstacleStats = new Obstacle (m_MaxHP, this.gameObject);
    }

    void OnCollisionEnter (Collision other) {
        BulletScript bulletScript = other.gameObject.GetComponent<BulletScript> ();

        if (bulletScript != null) {
            m_ObstacleStats.GetHit ();
        }
    }
}
