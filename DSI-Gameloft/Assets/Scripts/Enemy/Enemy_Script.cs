﻿using UnityEngine;
using System.Collections;

public class Enemy_Script : MonoBehaviour {
    #region Members
    // Playtest Only !
    public int m_DropTheKey = -1;

    public float m_Life;

    public bool m_IsAwake;

    public Transform m_Player;

    Renderer m_Renderer;

    public GameObject m_EnnemyMissile2;

    public float m_EarnedTime;
    public GameObject m_TimeDistributor;
    private TimeDistributor m_TimeDistributorScript;
    private GameObject m_KeyPrefab;
    #endregion

    public void Awake() {
        m_KeyPrefab = Resources.Load<GameObject>("Prefabs/Key/Key");
    }

    float m_FreezeDelay;
    protected bool m_IsFreeze;

    public virtual void Start() {
        m_Player =  GameObject.FindGameObjectWithTag("Player").transform;
        FloorManager.instance.NewEnemyAppeared();

        m_FreezeDelay = 3.0f;

        m_Renderer = this.GetComponent<Renderer>();
    }

    public void GetDamage(float m_Damage) {
        StartCoroutine(blink());
        m_Life -= m_Damage;
        if (m_Life <= 0) {
            this.DestroyEnemy();
        }
    }

    public IEnumerator FreezeEnemy() {
        m_IsFreeze = true;
        yield return new WaitForSeconds(m_FreezeDelay);
        m_IsFreeze = false;
    }



    public void DestroyEnemy() {
        bool mustPopKey = m_DropTheKey > -1
            ? m_DropTheKey > 0
            : FloorManager.instance.MustPopKey();

        if (name == "EnemyMissile" && m_EnnemyMissile2 != null) {
            GameObject firstChild = Instantiate(m_EnnemyMissile2, this.transform.position, this.transform.rotation) as GameObject;
            GameObject secondChild = Instantiate(m_EnnemyMissile2, this.transform.position, this.transform.rotation) as GameObject;

            firstChild.GetComponent<Enemy_Script>().m_DropTheKey = m_DropTheKey > 0
                ? 0 : m_DropTheKey;
            secondChild.GetComponent<Enemy_Script>().m_DropTheKey = m_DropTheKey > 0
                ? 0 : m_DropTheKey;
        }

        m_Player.GetComponent<PlayerScript>().Unlock();


        GameObject m_PrefabTimeDistributor = Instantiate(m_TimeDistributor, this.transform.position, this.transform.rotation) as GameObject;
        m_TimeDistributorScript = m_PrefabTimeDistributor.GetComponent<TimeDistributor>();
        m_TimeDistributorScript.m_EarnTime = m_EarnedTime;

        if (mustPopKey) {
            PopKey();
        }

        Destroy(this.gameObject);
    }

    public virtual void Update() {

        if (m_Renderer.IsVisibleFrom(Camera.main)) {
            m_IsAwake = true;
            m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else {
            m_IsAwake = false;
        }

        transform.LookAt(m_Player, Vector3.up);



    }

    public IEnumerator blink(float time = 0.5f) {
        float delay = 0.15f;

        while (time > 0) {

            if (m_Renderer.material.GetFloat("Dommages")==0) {
                m_Renderer.material.SetFloat("Dommages", 1);
            }
            else {
                m_Renderer.material.SetFloat("Dommages", 0);
            }

            yield return new WaitForSeconds(delay);

            time -= delay;
        }

        m_Renderer.material.SetFloat("Dommages", 0);
        yield return null;
    }

    void PopKey() {
        Object.Instantiate(m_KeyPrefab, this.transform.position, Quaternion.identity);
    }
}
