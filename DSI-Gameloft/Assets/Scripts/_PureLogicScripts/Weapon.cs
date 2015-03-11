﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon {
    #region Members
    public int m_Power;
    public float m_FireRate;
    public bool m_IsAlly;
    public bool m_IsHoming;
    #endregion

    public Weapon ()
        : this (1, 0.5f, true, false) { }

    public Weapon (int power, float fireRate, bool isAlly, bool isHoming) {
        m_Power = power;
        m_FireRate = fireRate;
        m_IsAlly = isAlly;
        m_IsHoming= isHoming;
    }
}
