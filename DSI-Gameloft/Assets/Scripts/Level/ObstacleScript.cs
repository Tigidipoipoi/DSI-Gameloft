using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {
    #region Members
    public int m_MaxHP = 3;

    Obstacle m_ObstacleStats;
    EnemyLock m_EnemyLockScript;
    #endregion

    void Start () {
        m_EnemyLockScript = this.GetComponent<EnemyLock> ();
        m_ObstacleStats = new Obstacle (m_MaxHP, this.gameObject);
    }

    void OnTriggerEnter (Collider other) {
        BulletScript bulletScript = other.GetComponent<BulletScript> ();
        if (bulletScript != null) {
            m_ObstacleStats.GetHit ();
        }
    }
}
