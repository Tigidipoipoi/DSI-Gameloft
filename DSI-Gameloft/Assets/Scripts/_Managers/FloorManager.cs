using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {
    #region Singleton
    static private FloorManager s_Instance;
    static public FloorManager instance {
        get {
            return s_Instance;
        }
    }

    void Awake () {
        if (s_Instance == null)
            s_Instance = this;
        DontDestroyOnLoad (this);
        this.Init ();
    }
    #endregion

    #region Members
    public int m_RemainingEnemiesCount;
    public GameObject[] m_FloorPrefabs;
    public FloorScript m_CurrentFloor;
    public bool m_KeyPoped;
    public bool m_KeyAquired;
    #endregion

    public void Init () {
        m_KeyPoped = false;
        m_KeyAquired = false;
    }

    public bool MustPopKey () {
        --m_RemainingEnemiesCount;
        if (m_KeyPoped
            || m_KeyAquired) {
            return false;
        }

        int popRate = 0;
        if (m_RemainingEnemiesCount < 10) {
            popRate = 110 - 10 * m_RemainingEnemiesCount;
        }
        else {
            popRate = 100 / m_RemainingEnemiesCount;
        }
        int rng = Random.Range (0, 99);

        bool mustPopKey = popRate > rng;
        if (mustPopKey) {
            m_KeyPoped = true;
        }

        return mustPopKey;
    }

    public void NewEnemyAppeared () {
        ++m_RemainingEnemiesCount;
    }
}
