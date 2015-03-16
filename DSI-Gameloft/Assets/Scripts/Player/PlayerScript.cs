using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerScript : MonoBehaviour {
    #region Members
    public bool m_IsInTurretMode;

    [HideInInspector]
    public float c_PlayerPosYClamp;
    [HideInInspector]
    public Transform m_EnemyTarget;

    Transform[] m_WeaponsSlot;

    public WeaponScript[] m_Weapons;
    [HideInInspector]
    public Renderer m_GatlingRenderer;
    IEnumerator[] m_WeaponCoroutines;
    #endregion

    void Start() {
        m_GatlingRenderer = this.transform.FindChild("M_AV_gatling").GetComponent<Renderer>();

        c_PlayerPosYClamp = this.transform.position.y;

        m_WeaponCoroutines = new IEnumerator[m_Weapons.Length];

        Transform weaponsTrans = this.transform.FindChild("Weapons");
        int slotCount = weaponsTrans.childCount;
        m_WeaponsSlot = new Transform[slotCount];
        for (int i = 0; i < slotCount; ++i) {
            m_WeaponsSlot[i] = weaponsTrans.GetChild(i);
        }

        int weaponCount = m_Weapons.Length;
        if (m_Weapons == null
            || weaponCount > slotCount) {
            m_Weapons = new WeaponScript[slotCount];
        }

        this.UpdateWeaponsCoroutines();
    }

    void Update() {
        // Look at locked enemy
        if (m_EnemyTarget != null) {
            Vector3 lookAtTarget = m_EnemyTarget.position;
            lookAtTarget.y = c_PlayerPosYClamp;
            this.transform.LookAt(m_EnemyTarget.position);
        }

        // Turret mode
        if (m_IsInTurretMode) {
            this.LookAtMouse();
        }
    }

    public void LockTarget(EnemyLock enemyLockScript) {
        if (m_EnemyTarget != null) {
            return;
        }

        m_EnemyTarget = enemyLockScript.transform;
        enemyLockScript.m_IsPlayerTarget = true;

        Vector3 lookAtTarget = m_EnemyTarget.position;
        lookAtTarget.y = c_PlayerPosYClamp;
        this.transform.LookAt(m_EnemyTarget.position);

        int weaponCount = m_Weapons.Length;
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].StartCoroutine(m_WeaponCoroutines[i]);
            }
        }
    }

    public void Unlock() {
        if (m_EnemyTarget == null) {
            return;
        }

        m_EnemyTarget.GetComponent<EnemyLock>().m_IsPlayerTarget = false;
        m_EnemyTarget = null;

        int weaponCount = m_Weapons.Length;
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].StopCoroutine(m_WeaponCoroutines[i]);
            }
        }
    }

    public void UpdateWeaponsCoroutines() {
        int weaponCount = m_Weapons.Length;
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_WeaponCoroutines[i] = m_Weapons[i].AutoFire();
            }
        }
    }

    public void UpdateWeaponsHoming(bool isHoming) {
        int weaponCount = m_Weapons.Length;
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].m_WeaponStats.m_IsHoming = isHoming;
            }
        }
    }

    public IEnumerator TurretShoot() {
        int weaponCount = m_Weapons.Length;
        this.LookAtMouse();
        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].StartCoroutine(m_WeaponCoroutines[i]);
            }
        }

        while (m_IsInTurretMode) {
            ShakeManager.instance.LetsShake(300);
            yield return null;
        }

        for (int i = 0; i < weaponCount; ++i) {
            if (m_Weapons[i] != null) {
                m_Weapons[i].StopCoroutine(m_WeaponCoroutines[i]);
            }
        }
    }

    void LookAtMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        Vector3 lookTarget = hit.point;
        lookTarget.y = c_PlayerPosYClamp;

        this.transform.LookAt(lookTarget);
    }

    public void LootWeapon(WeaponScript lootWeapon) {
        WeaponScript sameWeaponType = null;

        int slotCount = m_WeaponsSlot.Length;
        for (int i = 0; i < slotCount; ++i) {
            if (m_Weapons[i] == null) {
                continue;
            }

            if (m_Weapons[i].m_Type == lootWeapon.m_Type) {
                sameWeaponType = m_Weapons[i];
            }
        }

        if (sameWeaponType != null) {
            // Upgrade
            return;
        }

        for (int i = 0; i < slotCount; ++i) {
            if (m_WeaponsSlot[i].childCount == 0) {
                lootWeapon.transform.position = m_WeaponsSlot[i].position;
                lootWeapon.transform.rotation = m_WeaponsSlot[i].rotation;
                lootWeapon.transform.parent = m_WeaponsSlot[i];
                m_Weapons[i] = lootWeapon;

                if (lootWeapon.m_Type == WeaponScript.WEAPON_TYPE.GATLING) {
                    m_GatlingRenderer.enabled = true;
                }

                this.UpdateWeaponsCoroutines();
                return;
            }
        }
    }
}
