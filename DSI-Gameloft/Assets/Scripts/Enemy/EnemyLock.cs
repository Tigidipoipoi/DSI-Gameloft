using UnityEngine;
using System.Collections;

public class EnemyLock : MonoBehaviour {
    #region Members
    public bool m_IsPlayerTarget;

    Renderer m_Renderer;

    PlayerScript m_PlayerScript;
    #endregion

    void Start () {
        m_PlayerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
        m_Renderer = this.GetComponent<Renderer> ();
    }

    void Update () {
        if (m_IsPlayerTarget
            && !m_Renderer.IsVisibleFrom (Camera.main)) {
            m_PlayerScript.Unlock ();
        }
    }
}
