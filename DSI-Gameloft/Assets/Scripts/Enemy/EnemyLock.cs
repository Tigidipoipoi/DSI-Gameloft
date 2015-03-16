using UnityEngine;
using System.Collections;

public class EnemyLock : MonoBehaviour {
    #region Members
    public bool m_IsPlayerTarget;

    public Renderer m_Renderer;

    PlayerScript m_PlayerScript;
    #endregion

    void Start() {
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (m_Renderer == null) {
            m_Renderer = this.GetComponent<Renderer>();
        }
    }

    void Update() {
        if (m_IsPlayerTarget
            && !m_Renderer.IsVisibleFrom(Camera.main)) {
            m_PlayerScript.Unlock();
        }
    }
}
