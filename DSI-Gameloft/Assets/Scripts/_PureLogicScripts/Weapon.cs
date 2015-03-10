using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon {
    #region Members
    public int m_Power;
    public float m_FireRate;
    public bool m_IsAlly;
    #endregion

    public Weapon ()
        : this (1, 0.5f, true) { }

    public Weapon (int power, float fireRate, bool isAlly) {
        m_Power = power;
        m_FireRate = fireRate;
        m_IsAlly = isAlly;
    }
}
