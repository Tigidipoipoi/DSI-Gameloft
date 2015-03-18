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
    }
    #endregion

    #region Members
    Transform m_IGPanel;
    // Temp
    Text m_TimerText;
    // Pesos
    Text m_PesosText;
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
    public AnimationCroix m_AnimationCroix;

    AudioSource m_AudioSource;

    //Combo Chest
    public int m_ComboChest;
    bool m_ComboChestCompteur;
    float m_ComboChestTime = 20.0f;

    //Barre de temps
    private float m_Time;
    private bool m_TimeBlink;
    private int m_TimeSizeMin = 16;
    private int m_TimeSizeMax = 20;

    // GameOver
    private Transform m_GameOverPanel;
    #endregion

    void Start() {
        m_AudioSource = GetComponent<AudioSource>();

        Transform canvasTrans = GameObject.Find("Canvas").transform;
        m_IGPanel = canvasTrans.transform.FindChild("IGPanel");
        m_TimerText = m_IGPanel.FindChild("RemainingTime").GetComponent<Text>();
        m_TimerText.color = Color.white;
        m_TimerText.fontSize = m_TimeSizeMin;

        m_IGPanel = canvasTrans.transform.FindChild("PesosPanel");
        m_PesosText = m_IGPanel.FindChild("Pesos").GetComponent<Text>();
        m_PesosText.color = Color.white;
        m_PesosText.fontSize = m_TimeSizeMin;
        m_PesosText.text = string.Format("{0}", "0");
        m_ComboKill = 0;
        m_ComboChest = 0;
        ComboKillCoroutine = ComboKillTimer();

        // GameOver transforms
        m_GameOverPanel = canvasTrans.FindChild("GameOverPanel");
        m_GameOverPanel.gameObject.SetActive(false);
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
            if (!m_AudioSource.isPlaying) {
                m_AudioSource.Play();
            }

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

    public void UpdatePesos(int newPesos, int pesos) {
        if (m_PesosText == null) {
            return;
        }

        StartCoroutine(AddPesos(newPesos, pesos));
    }

    IEnumerator AddPesos(int newPesos, int pesos) {
        if (m_ComboChest > 1) {
            newPesos = newPesos * m_ComboChest;
        }

        for (int i = 0; i < newPesos; i++) {
            pesos++;

            if (pesos % 2 == 1) {
                m_PesosText.color = Color.yellow;
                m_PesosText.fontSize = 20;
            }
            else {
                m_PesosText.color = Color.white;
                m_PesosText.fontSize = 16;
            }

            m_PesosText.text = string.Format("{0}", pesos.ToString("0"));
            yield return new WaitForSeconds(0.2f);
        }

        m_PesosText.color = Color.white;
        m_PesosText.fontSize = m_TimeSizeMin;
        PesosManager.instance.m_Pesos = pesos;
        m_PesosText.text = string.Format("{0}", PesosManager.instance.m_Pesos.ToString("0"));
    }

    IEnumerator TimeBlink() {
        m_TimeBlink = true;

        while (m_Time < 20 && m_Time > 0) {
            Debug.Log(m_Time);
            m_TimerText.color = Color.red;
            m_TimerText.fontSize = m_TimeSizeMax;
            yield return new WaitForSeconds(1);
            m_TimerText.color = Color.white;
            m_TimerText.fontSize = m_TimeSizeMin;
            yield return new WaitForSeconds(1);
            if (m_Time < 1) {
                m_Time = 0;
            }
        }
        if (m_AudioSource.isPlaying) {
            m_AudioSource.Stop();
        }
        m_TimeBlink = false;
    }

    public void DisplayGameOverScreen(bool catIsDead) {
        m_GameOverPanel.gameObject.SetActive(true);
        Transform screenTrans = null;
        Transform timeTrans = m_GameOverPanel.FindChild("RemainingTime").FindChild("Text");
        Transform cactusTrans = m_GameOverPanel.FindChild("CactusKilled").FindChild("Text");
        Transform chestsTrans = m_GameOverPanel.FindChild("Chests").FindChild("Text");
        Transform goldTrans = m_GameOverPanel.FindChild("GoldEarned").FindChild("Text");
        Transform scoreTrans = m_GameOverPanel.FindChild("Score");

        if (catIsDead) {
            screenTrans = m_GameOverPanel.FindChild("Lost");
        }
        else {
            screenTrans = m_GameOverPanel.FindChild("Won");

            int rescuedCat = PlayerPrefs.GetInt("RescuedCats", 0);
            PlayerPrefs.SetInt("RescuedCats", rescuedCat + 1);
        }

        float remainingTime = TimerManager.instance.m_RemainingTime;
        if (remainingTime < 0.0f)
            remainingTime = 0.0f;

        timeTrans.GetComponent<Text>().text = remainingTime.ToString("00.0");
        cactusTrans.GetComponent<Text>().text = FloorManager.instance.m_KilledEnemiesCount.ToString();
        chestsTrans.GetComponent<Text>().text = FloorManager.instance.m_OpenedChestsCount.ToString();
        goldTrans.GetComponent<Text>().text = PesosManager.instance.m_Pesos.ToString();
        scoreTrans.GetComponent<Text>().text = FloorManager.instance.ComputeScore().ToString();

        screenTrans.gameObject.SetActive(true);

        Time.timeScale = 0.0f;
    }

    public void ContinueButton() {
        Time.timeScale = 1.0f;

        Application.LoadLevel("StartMenu");
    }
}
