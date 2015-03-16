using UnityEngine;
using System.Collections;

public class ConsumablesManager : MonoBehaviour {
    #region Singleton
    static private ConsumablesManager s_Instance;
    static public ConsumablesManager instance {
        get {
            return s_Instance;
        }
    }

    void Awake() {
        if (s_Instance == null)
            s_Instance = this;
        DontDestroyOnLoad(this);
        this.Init();
    }
    #endregion

    public bool m_FreezeEnemy;
    public bool m_KillAllEnemy;

    void Init() {
        m_FreezeEnemy = false;
        m_KillAllEnemy = false;
    }

    public IEnumerator LetsFreeze() {
        m_FreezeEnemy = true;

        yield return null;
        yield return null;
        m_FreezeEnemy = false;
    }

    public IEnumerator LetsKill() {
        m_KillAllEnemy = true;

        yield return null;
        yield return null;
        m_KillAllEnemy = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(LetsKill());
        }
    }
}
