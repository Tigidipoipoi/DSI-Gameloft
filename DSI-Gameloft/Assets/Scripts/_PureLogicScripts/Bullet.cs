﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Bullet {
    #region Members
    public float m_Power;
    public float m_Speed;
    #endregion

    public Bullet ()
        : this (1.0f, 20.0f) { }

    public Bullet (float power, float speed) {
        m_Power = power;
        m_Speed = speed;
    }
}
