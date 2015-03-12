using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    #region Singleton
    static private UIManager s_Instance;
    static public UIManager instance {
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
    Transform m_IGPanel;
    // Temp
    Text m_TimerText;
    #endregion

    void Start () {
        m_IGPanel = GameObject.Find ("Canvas").transform.FindChild ("IGPanel");
        m_TimerText = m_IGPanel.FindChild ("RemainingTime").GetComponent<Text> ();
    }

    public void Init () {
    }

    public void UpdateRemainingTime (float remainingTime) {
        if (m_TimerText == null) {
            return;
        }

        m_TimerText.text = string.Format ("Remaining time: {0}", remainingTime.ToString ("00.0"));
    }
}
