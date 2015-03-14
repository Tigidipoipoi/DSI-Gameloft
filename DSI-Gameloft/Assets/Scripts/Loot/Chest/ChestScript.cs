﻿using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

    private bool m_ChestOpen;

    public GameObject m_ChestLoot;

    private Collider m_ChestCollider;

    public int m_EarnPesos;

    public GameObject m_ObjectParticules;

    private Animation m_Animation;
    AudioSource m_Audio;

	// Use this for initialization
	void Start () {
        m_ChestOpen = false;
        m_ChestCollider = this.gameObject.GetComponent<Collider>();
        m_Animation = GetComponent<Animation>();
        m_Audio = GetComponent<AudioSource>();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            m_Audio.Play();
            m_ChestOpen = true;
            m_ChestCollider.enabled = false;
            m_Animation.Play();
            m_ObjectParticules.SetActive(true);
            
            if(m_ChestLoot!=null)
            {
             Instantiate(m_ChestLoot, this.transform.position + Random.insideUnitSphere, this.transform.rotation);
            }
            
            PesosManager.instance.AddPesos(m_EarnPesos);
            Destroy(this);
        }
    }
}
