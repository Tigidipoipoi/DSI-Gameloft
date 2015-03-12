using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {
    #region Singleton
    static private TimerManager s_Instance;
    static public TimerManager instance {
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
    float m_RemainingTime;
    int m_FloorTime = 60;
    IEnumerator m_TimeIsRunningOut;
    #endregion

    public void Init () {
        m_RemainingTime = m_FloorTime;
        m_TimeIsRunningOut = TimeIsRunningOut ();
        this.StartCoroutine (m_TimeIsRunningOut);
    }

    IEnumerator TimeIsRunningOut () {
        // Wait for rhe start
        yield return null;

        while (m_RemainingTime >= 0.0f) {
            m_RemainingTime -= Time.deltaTime;
<<<<<<< HEAD:DSI-Gameloft/Assets/Scripts/_Managers/TimerManager.cs
            UIManager.instance.UpdateRemainingTime (m_RemainingTime);
=======
            //Debug.Log (m_RemainingTime.ToString ());
>>>>>>> ad65a806500c172fb8666e63961a45a95eec7bc9:DSI-Gameloft/Assets/Scripts/Timer/TimerManager.cs
            yield return null;
        }

        // Run Over
    }

    public void AddTime (float timeEarned) {
        m_RemainingTime += timeEarned;
    }

    public void LoseTime (float timeLost) {
        m_RemainingTime -= timeLost;
    }
}
