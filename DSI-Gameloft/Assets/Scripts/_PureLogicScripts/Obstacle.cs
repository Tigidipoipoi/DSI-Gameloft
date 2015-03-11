using UnityEngine;
using System.Collections;

[System.Serializable]
public class Obstacle {
    #region Members
    public int m_HP;
    public GameObject m_GO;
    #endregion

    public Obstacle ()
        : this (3, null) { }

    public Obstacle (int hp, GameObject go) {
        m_HP = hp;
        m_GO = go;
    }

    public void GetHit () {
        --m_HP;

        if (m_HP <= 0) {
            this.Explode ();
        }
    }

    void Explode () {
        Debug.Log ("Boom!");
        Object.Destroy (m_GO);
    }
}
