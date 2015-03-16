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
        //DontDestroyOnLoad (this);
        this.Init ();
    }
    #endregion

    #region Members
    public float m_RemainingTime;
    int m_FloorTime = 10;
    IEnumerator m_TimeIsRunningOut;

    public GameObject m_TimeWhite;
    private float m_TimePourcentage;
    private float pixel = 0.05f;
    private bool IsBlinking;

    private SpriteRenderer m_Sprite;

    AudioSource m_AudioSource;

    #endregion

    public void Init () {
        m_RemainingTime = m_FloorTime;
        m_TimeIsRunningOut = TimeIsRunningOut ();
        this.StartCoroutine (m_TimeIsRunningOut);
        m_TimePourcentage = (m_RemainingTime * 100) / m_FloorTime;
        m_Sprite = m_TimeWhite.GetComponent<SpriteRenderer>();
        m_AudioSource=GetComponent<AudioSource>();
    }

    IEnumerator TimeIsRunningOut () {
        // Wait for the start
        yield return null;

        while (m_RemainingTime >= 0.0f) {
            m_RemainingTime -= Time.deltaTime;
            if (UIManager.instance != null) {
                UIManager.instance.UpdateRemainingTime(m_RemainingTime, m_FloorTime, m_TimePourcentage);
                m_TimePourcentage = (m_RemainingTime * 100) / m_FloorTime;
               
                m_TimeWhite.transform.localPosition = new Vector3((m_TimePourcentage * pixel) - 5.16f, 0, 0);

                if(m_TimePourcentage<20 && IsBlinking == false)
                {
                    StartCoroutine(BlinkBar());
                }


            }
            yield return null;
        }

        //DEATH
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }

    IEnumerator BlinkBar()
    {
        IsBlinking = true;
        while (m_TimePourcentage < 20)
        {
            m_Sprite.color = Color.red;
            yield return new WaitForSeconds(1);
            m_Sprite.color = Color.white;
            yield return new WaitForSeconds(1);

        }
        m_Sprite.color = Color.white;

        IsBlinking = false;
    }

    IEnumerator AddTimeBlink()
    {
            m_Sprite.color = Color.yellow;
            yield return new WaitForSeconds(0.3f);
            m_Sprite.color = Color.white;
    }
    IEnumerator LoseTimeBlink()
    {
        m_Sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        m_Sprite.color = Color.white;
        Handheld.Vibrate();
    }


    public void AddTime (float timeEarned) {
        m_RemainingTime += timeEarned;
        if(m_RemainingTime>m_FloorTime)
        {
            m_RemainingTime = m_FloorTime;
        }
        m_TimePourcentage = (m_RemainingTime * 100) / m_FloorTime;
        m_TimeWhite.transform.localPosition = new Vector3((m_TimePourcentage * pixel) - 5.16f, 0, 0);
        StartCoroutine(AddTimeBlink());
    }

    public void LoseTime (float timeLost) {
        m_RemainingTime -= timeLost;
        m_TimePourcentage = (m_RemainingTime * 100) / m_FloorTime;
        m_TimeWhite.transform.localPosition = new Vector3((m_TimePourcentage * pixel) - 5.16f, 0, 0);
        StartCoroutine(LoseTimeBlink());
    }


}
