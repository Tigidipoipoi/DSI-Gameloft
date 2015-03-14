using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
    #region Members
    public const int c_MaxWeaponCount = 3;
    public bool m_IsInTurretMode;

    [HideInInspector]
    public Transform m_EnemyTarget;

    public WeaponScript[] m_Weapons;
    IEnumerator[] m_WeaponCoroutines;
    #endregion

    void Start () {
        int weaponCount = m_Weapons.Length;
        if (m_Weapons == null
            || weaponCount > c_MaxWeaponCount) {
            m_Weapons = new WeaponScript[c_MaxWeaponCount];
        }

        m_WeaponCoroutines = new IEnumerator[m_Weapons.Length];

        this.UpdateWeaponsCoroutines ();
    }

    void Update () {
        // Look at locked enemy
        if (m_EnemyTarget != null) {
            this.transform.LookAt (m_EnemyTarget.position);
        }

        // Turret mode
        if (m_IsInTurretMode) {
            this.LookAtMouse ();
        }
    }


    public void LockTarget (EnemyLock enemyLockScript) {
        if (m_EnemyTarget != null) {
            return;
        }

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

    public void UpdateWeaponsHoming (bool isHoming) {
        int weaponCount = m_Weapons.Length;
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].m_WeaponStats.m_IsHoming = isHoming;
            }
        }
    }

    public IEnumerator TurretShoot () {

        int weaponCount = m_Weapons.Length;
        this.LookAtMouse ();
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].StartCoroutine (m_WeaponCoroutines[i]);
            }
        }

        while (m_IsInTurretMode) {
            ShakeManager.instance.LetsShake(300);
            yield return null;
        }

        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].StopCoroutine (m_WeaponCoroutines[i]);
            }
        }
    }

    void LookAtMouse () {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast (ray, out hit, Mathf.Infinity);
        Vector3 lookTarget = hit.point;
        lookTarget.y = 1.0f;

        this.transform.LookAt (lookTarget);
    }
}
