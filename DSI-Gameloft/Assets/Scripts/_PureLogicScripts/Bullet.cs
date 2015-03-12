using UnityEngine;
using System.Collections;

[System.Serializable]
public class Bullet {
    #region Members
    public float m_Power;
    public int m_Speed;
    #endregion

    public Bullet ()
        : this (1.0f, 20) { }

    public Bullet (float power, int speed) {
        m_Power = power;
        m_Speed = speed;
    }
}
