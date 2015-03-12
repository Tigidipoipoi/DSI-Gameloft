using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon {
    #region Members
    public float m_Power;
    public float m_FireRate;
    public float m_HoldFireRate;
    public bool m_IsAlly;
    public bool m_IsHoming;
    public int m_CurrentLevel;
    #endregion

    public Weapon ()
        : this (1.0f, 0.5f, 0.25f, true, false, 0) { }

    public Weapon (float power, float fireRate, float holdFireRate, bool isAlly, bool isHoming, int level) {
        m_Power = power;
        m_FireRate = fireRate;
        m_HoldFireRate = holdFireRate;
        m_IsAlly = isAlly;
        m_IsHoming = isHoming;
        m_CurrentLevel = level;
    }
}
