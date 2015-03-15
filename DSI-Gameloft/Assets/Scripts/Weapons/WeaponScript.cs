using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
    public enum WEAPON_TYPE {
        NONE = 0,
        GUN,
        SHOT_GUN,
        GATLING,

        COUNT
    }

    #region Members
    public Weapon m_WeaponStats;
    public GameObject m_BulletPrefab;
    public GameObject m_HomingBulletPrefab;
    public Transform m_BulletSpawn;
    public GameObject m_Holder;

    protected int m_BulletLayer;
    protected PlayerScript m_PlayerScript;
    public WEAPON_TYPE m_Type;
    #endregion

    protected virtual void Start() {
        m_BulletLayer = LayerMask.NameToLayer(m_WeaponStats.m_IsAlly
            ? "AllyBullet"
            : "EnemyBullet");
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        if (m_Type == null) {
            m_Type = WEAPON_TYPE.NONE;
        }
    }

    public virtual IEnumerator AutoFire() {
        while (true) {
            this.Fire();
            yield return new WaitForSeconds(m_PlayerScript.m_IsInTurretMode
                ? m_WeaponStats.m_HoldFireRate : m_WeaponStats.m_FireRate);
        }
    }

    public virtual void Fire() {
        GameObject bulletGO = Object.Instantiate(m_WeaponStats.m_IsHoming ? m_HomingBulletPrefab : m_BulletPrefab,
            m_BulletSpawn.position, m_BulletSpawn.rotation) as GameObject;
        bulletGO.layer = m_BulletLayer;
        bulletGO.GetComponent<BulletScript>().m_BulletStats.m_Power = m_WeaponStats.m_Power;

        if (m_WeaponStats.m_IsHoming) {
            if (m_Holder.tag == "Player") {
                bulletGO.GetComponent<HomingBulletScript>().m_Target = m_PlayerScript.m_EnemyTarget;
            }
            else {
                bulletGO.GetComponent<HomingBulletScript>().m_Target = m_PlayerScript.transform;
            }
        }
    }

    public virtual void LevelUpWeapon() {
        if (m_WeaponStats.m_CurrentLevel <= 2) {
            ++m_WeaponStats.m_CurrentLevel;
        }
    }
}
