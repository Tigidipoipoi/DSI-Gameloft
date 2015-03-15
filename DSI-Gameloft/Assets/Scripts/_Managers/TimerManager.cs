﻿using UnityEngine;
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

    public float m_TimeMax;
    public GameObject m_TimeWhite;
    
    #endregion

    public void Init () {
        m_RemainingTime = m_FloorTime;
        m_TimeIsRunningOut = TimeIsRunningOut ();
        this.StartCoroutine (m_TimeIsRunningOut);
    }

    IEnumerator TimeIsRunningOut () {
        // Wait for the start
        yield return null;

        while (m_RemainingTime >= 0.0f) {
            m_RemainingTime -= Time.deltaTime;
            if (UIManager.instance != null) {
                UIManager.instance.UpdateRemainingTime (m_RemainingTime);
            }
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
