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

    void Awake() {
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
        this.Init();
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

    //Animation
    //Général
    public bool m_IsNewComboChest;

    public AnimationComboChest m_AnimationComboChest;
    public AnimationComboChestLetters m_AnimationComboChestLetters;
    public AnimationNumbers m_AnimationNumbers;
    public AnimationCrane m_AnimationCrane;
    public AnimationCroix m_AnimationCroix;

    //Combo Chest
    public int m_ComboChest;
    bool m_ComboChestCompteur;
    float m_ComboChestTime = 20.0f;
    IEnumerator ComboChestCoroutine;

    //Barre de temps
    private float m_Time;
    private bool m_TimeBlink;
    private int m_TimeSizeMin = 16;
    private int m_TimeSizeMax = 20;
    #endregion

    void Start() {
        m_IGPanel = GameObject.Find("Canvas").transform.FindChild("IGPanel");
        m_TimerText = m_IGPanel.FindChild("RemainingTime").GetComponent<Text>();
        m_TimerText.color = Color.white;
        m_TimerText.fontSize = m_TimeSizeMin;
        m_ComboKill = 0;
        m_ComboChest = 0;
        ComboKillCoroutine = ComboKillTimer();
        ComboChestCoroutine = ComboChestTimer();
    }

    public void Init() {

    }

    #region ComboKill
    public void AddComboKill() {
        m_ComboKill++;
        if (m_ComboKillCompteur == false) {
            m_ComboKillCompteur = true;
            StartCoroutine(ComboKillCoroutine);
        }
        else {
            StopCoroutine(ComboKillCoroutine);
            StartCoroutine(ComboKillCoroutine);
        }
    }

    IEnumerator ComboKillTimer() {
        yield return new WaitForSeconds(m_ComboKillTime);
        m_ComboKillCompteur = false;
        m_ComboKill = 0;
    }
    #endregion

    #region ComboChest
    public void AddComboChest() {
        m_ComboChest++;
        Debug.Log(m_ComboChest.ToString());
        if (m_ComboChest >= 2) {


            m_AnimationComboChest.StartCombo(true);
            m_AnimationNumbers.StartCombo(m_ComboChest);
            m_AnimationComboChestLetters.StartCombo();
            m_AnimationCroix.StartCombo();
        }
        if (m_ComboChestCompteur == false) {

            m_ComboChestCompteur = true;
            StartCoroutine("ComboChestTimer");
        }
        else {
            StopCoroutine("ComboChestTimer");
            StartCoroutine("ComboChestTimer");
        }
    }

    IEnumerator ComboChestTimer() {

        yield return new WaitForSeconds(m_ComboChestTime);

        m_ComboChestCompteur = false;


        if (m_ComboChest >= 2) {
            m_AnimationNumbers.StartCombo(m_ComboChest);
            m_AnimationComboChest.StartCombo(false);
        }
        m_ComboChest = 0;
    }
    #endregion

    public void UpdateRemainingTime(float remainingTime, float maxtime, float pourcentagetime) {
        if (m_TimerText == null) {
            return;
        }

        m_Time = pourcentagetime;

        if (pourcentagetime < 20 && pourcentagetime > 0 && m_TimeBlink == false) {
            StartCoroutine(TimeBlink());
        }
        else if (m_TimeBlink == false) {
            m_TimerText.color = Color.white;
            m_TimerText.fontSize = m_TimeSizeMin;
        }
        if (pourcentagetime <= 0) {
            m_TimerText.color = Color.red;
            m_TimerText.fontSize = m_TimeSizeMax;
        }
        m_TimerText.text = string.Format("{0}", remainingTime.ToString("00.0"));
    }

    IEnumerator TimeBlink() {
        m_TimeBlink = true;
        while (m_Time < 20 && m_Time > 0) {
            m_TimerText.color = Color.red;
            m_TimerText.fontSize = m_TimeSizeMax;
            yield return new WaitForSeconds(1);
            m_TimerText.color = Color.white;
            m_TimerText.fontSize = m_TimeSizeMin;
            yield return new WaitForSeconds(1);
        }
        m_TimeBlink = false;
    }


}
