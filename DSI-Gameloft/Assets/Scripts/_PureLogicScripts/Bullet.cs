using UnityEngine;
using System.Collections;

[System.Serializable]
public class Bullet {
    #region Members
    public int m_Power;
    public int m_Speed;
    #endregion

    public Bullet ()
        : this (1, 100) { }

    public Bullet (int power, int speed) {
        m_Power = power;
        m_Speed = speed;
    }
}
