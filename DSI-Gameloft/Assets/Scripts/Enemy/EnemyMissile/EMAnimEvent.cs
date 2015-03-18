﻿using UnityEngine;
using System.Collections;

public class EMAnimEvent : MonoBehaviour {
    EnemyMissile m_EMScript;

    public void Start() {
        m_EMScript = this.transform.parent.GetComponent<EnemyMissile>();
    }

    public void Shoot() {
        Debug.Log("Call");
        m_EMScript.StartCoroutine("Shoot");
    }
}