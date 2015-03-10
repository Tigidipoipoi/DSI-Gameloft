using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
    #region Members
    public const int c_MaxWeaponCount = 3;

    Transform m_EnemyTarget;

    public WeaponScript[] m_Weapons;
    IEnumerator[] m_WeaponCoroutines;
    #endregion

    void Awake () {
        int weaponCount = m_Weapons.Length;
        if (m_Weapons == null
            || weaponCount > c_MaxWeaponCount) {
            m_Weapons = new WeaponScript[c_MaxWeaponCount];
        }

        m_WeaponCoroutines = new IEnumerator[m_Weapons.Length];

        UpdateWeaponsCoroutines ();
    }

    void Update () {
        if (m_EnemyTarget != null) {
            this.transform.LookAt (m_EnemyTarget.position);
        }
    }

    public void LockTarget (EnemyLock enemyLockScript) {
        m_EnemyTarget = enemyLockScript.transform;
        enemyLockScript.m_IsPlayerTarget = true;
        this.transform.LookAt (m_EnemyTarget.position);

        int weaponCount = m_Weapons.Length;
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].StartCoroutine (m_WeaponCoroutines[i]);
            }
        }
    }

    public void Unlock (EnemyLock enemyLockScript) {
        m_EnemyTarget = null;
        enemyLockScript.m_IsPlayerTarget = false;

        int weaponCount = m_Weapons.Length;
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].StopCoroutine (m_WeaponCoroutines[i]);
            }
        }
    }

    public void UpdateWeaponsCoroutines () {
        int weaponCount = m_Weapons.Length;
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_WeaponCoroutines[i] = m_Weapons[i].AutoFire ();
            }
        }
    }
}
