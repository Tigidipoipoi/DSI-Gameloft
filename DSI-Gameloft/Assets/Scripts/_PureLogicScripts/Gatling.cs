using UnityEngine;
using System.Collections;

[System.Serializable]
public class Gatling : Weapon {
    #region Members
    public float m_HoldPower;
    #endregion

    public Gatling ()
        : this (1.0f, 0.1f, 0.5f, true, false, 0) { }

    public Gatling (float power, float holdPower, float fireRate, bool isAlly, bool isHoming, int level) {
        m_Power = power;
        m_HoldPower = holdPower;
        m_FireRate = fireRate;
        m_IsAlly = isAlly;
        m_IsHoming = isHoming;
        m_CurrentLevel = level;
    }
}
