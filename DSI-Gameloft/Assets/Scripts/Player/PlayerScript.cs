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
        // Look at locked enemy
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

    public void Unlock () {
        if (m_EnemyTarget == null) {
            return;
        }

        m_EnemyTarget.GetComponent<EnemyLock> ().m_IsPlayerTarget = false;
        m_EnemyTarget = null;

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
