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

    void OnCollisionEnter (Collision other) {
<<<<<<< HEAD
        BulletScript bulletScript = other.gameObject.GetComponent<BulletScript> ();
=======
        BulletScript bulletScript = other.gameObject.GetComponent<BulletScript>();
>>>>>>> fedf80e010b550df919d387c9cf030019927c0dc
        if (bulletScript != null) {
            m_ObstacleStats.GetHit ();
        }
    }
}
