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

    public WeaponScript[] m_Weapons;
    public Transform m_GunTrans;
    public Transform m_GatlingTrans;
    public Transform m_ShotgunTrans;
    public Renderer m_GatlingRenderer;
    IEnumerator[] m_WeaponCoroutines;

    public Transform m_UpBodyTrans;
    public Transform m_DownBodyTrans;

	AudioSource m_AudioSource;
	public AudioClip m_LootClip;
    #endregion

    void Start() {
		m_AudioSource = GetComponent<AudioSource>();
        c_PlayerPosYClamp = this.transform.position.y;

        m_WeaponCoroutines = new IEnumerator[m_Weapons.Length];

        this.UpdateWeaponsCoroutines();
    }

    void Update() {
        // Look at locked enemy
        if (m_EnemyTarget != null) {
            Vector3 lookAtTarget = m_EnemyTarget.position;
            lookAtTarget.y = c_PlayerPosYClamp;
            m_DownBodyTrans.LookAt(lookAtTarget);
            m_UpBodyTrans.LookAt(lookAtTarget);
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
        m_UpBodyTrans.LookAt(lookAtTarget);

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
        Vector3 lookAtTarget = hit.point;
        lookAtTarget.y = c_PlayerPosYClamp;

        m_DownBodyTrans.LookAt(lookAtTarget);
        m_UpBodyTrans.LookAt(lookAtTarget);
    }

    public void LootWeapon(WeaponScript lootWeapon) {
        WeaponScript sameWeaponType = null;

		m_AudioSource.clip = m_LootClip;
		m_AudioSource.Play();

        int slotCount = m_Weapons.Length;
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

        switch (lootWeapon.m_Type) {
            case WeaponScript.WEAPON_TYPE.GATLING:
                lootWeapon.transform.position = m_GatlingTrans.position;
                lootWeapon.transform.rotation = m_GatlingTrans.rotation;
                lootWeapon.transform.parent = m_GatlingTrans;
                break;
            case WeaponScript.WEAPON_TYPE.SHOT_GUN:
                lootWeapon.transform.position = m_ShotgunTrans.position;
                lootWeapon.transform.rotation = m_ShotgunTrans.rotation;
                lootWeapon.transform.parent = m_GatlingTrans;
                m_GatlingRenderer.enabled = true;
                Debug.Log("arg");
                break;
            case WeaponScript.WEAPON_TYPE.GUN:
                lootWeapon.transform.position = m_GunTrans.position;
                lootWeapon.transform.rotation = m_GunTrans.rotation;
                lootWeapon.transform.parent = m_GatlingTrans;
                break;
        }

        for (int i = 0; i < slotCount; ++i) {
            if (m_Weapons[i] == null) {
                m_Weapons[i] = lootWeapon;

                this.UpdateWeaponsCoroutines();
                return;
            }
        }
    }
}
