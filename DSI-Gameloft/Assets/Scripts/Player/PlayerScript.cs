using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
    #region Members
    public const int c_MaxWeaponCount = 3;

    Transform m_EnemyTarget;

    public WeaponScript[] m_WeaponList;
    PlayerMovement m_PlayerMovementScript;
    #endregion

    void Awake () {
        if (m_WeaponList == null
            || m_WeaponList.Length > c_MaxWeaponCount) {
            m_WeaponList = new WeaponScript[c_MaxWeaponCount];
        }
        m_PlayerMovementScript = this.GetComponent<PlayerMovement> ();
    }

    void Update () {
        if (m_EnemyTarget != null) {
            this.transform.LookAt (m_EnemyTarget.position);
        }
    }

    public void LockTarget (EnemyLock enemyLockScript) {
        m_EnemyTarget = enemyLockScript.transform;
        enemyLockScript.m_IsPlayerTarget = true;
    }

    public void Unlock (EnemyLock enemyLockScript) {
        m_EnemyTarget = null;
        enemyLockScript.m_IsPlayerTarget = false;
    }
}
