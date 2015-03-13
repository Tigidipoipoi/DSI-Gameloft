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
    //Combo Kill
    public int m_ComboKill;
    bool m_ComboKillCompteur;
    public float m_ComboKillTime;
    IEnumerator ComboKillCoroutine;

    public int m_ComboChest;
    bool m_ComboChestCompteur;
    public float m_ComboChestTime;
    IEnumerator ComboChestCoroutine;
    #endregion

    void Start () {
        m_IGPanel = GameObject.Find ("Canvas").transform.FindChild ("IGPanel");
        m_TimerText = m_IGPanel.FindChild ("RemainingTime").GetComponent<Text> ();
        m_ComboKill = 0;
        m_ComboChest = 0;
        ComboKillCoroutine = ComboKillTimer();
        //ComboChestCoroutine = 
    }

    public void Init () {
    }

    #region ComboKill
    public void AddComboKill()
    {
        m_ComboKill++;
        if(m_ComboKillCompteur==false)
        {
            m_ComboKillCompteur = true;
            StartCoroutine(ComboKillCoroutine);
        }
        else
        {
            StopCoroutine(ComboKillCoroutine);
            StartCoroutine(ComboKillCoroutine);
        }
    }

    IEnumerator ComboKillTimer ()
    {
        yield return new WaitForSeconds(m_ComboKillTime);
        m_ComboKillCompteur = false;
        m_ComboKill = 0;
    }
    #endregion

    #region ComboChest
    public void AddComboChest()
    {
        m_ComboChest++;
        if (m_ComboChestCompteur == false)
        {
            m_ComboChestCompteur = true;
            StartCoroutine(ComboChestCoroutine);
        }
        else
        {
            StopCoroutine(ComboChestCoroutine);
            StartCoroutine(ComboChestCoroutine);
        }
    }

    IEnumerator ComboChestTimer()
    {
        yield return new WaitForSeconds(m_ComboChestTime);
        m_ComboChestCompteur = false;
        m_ComboChest = 0;
    }
    #endregion

    public void UpdateRemainingTime (float remainingTime) {
        if (m_TimerText == null) {
            return;
        }

        m_TimerText.text = string.Format ("Remaining time: {0}", remainingTime.ToString ("00.0"));
    }
}
