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

    AudioSource m_AudioSource;
    public AudioClip m_AudioGun;
    public AudioClip m_AudioGatline;
    public AudioClip m_AudioShotgun;
    public AudioClip m_AudioTurrel;
    #endregion

    protected virtual void Start() {
        m_AudioSource = GetComponent<AudioSource>();
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

        //
		
        switch (m_Type) {
            case WEAPON_TYPE.GUN:
				Debug.Log("a");
				EventManagerScript.emit(EventManagerType.GUN_SHOOT, this.transform.FindChild("BulletSpawn").gameObject);
                if (m_PlayerScript.m_IsInTurretMode) {
                    m_AudioSource.clip = m_AudioGun;
					EventManagerScript.emit(EventManagerType.TOURELLE, m_BulletSpawn.gameObject);
                }
                else {
                    m_AudioSource.clip = m_AudioGun;
                }
                break;

            case WEAPON_TYPE.GATLING:
				EventManagerScript.emit(EventManagerType.GATLINE_SHOOT, this.transform.FindChild("BulletSpawn").gameObject);
                if (m_PlayerScript.m_IsInTurretMode) {
                    m_AudioSource.clip = m_AudioGatline;
					EventManagerScript.emit(EventManagerType.TOURELLE, m_BulletSpawn.gameObject);
                }
                else {
                    m_AudioSource.clip = m_AudioGatline;
                }
                break;

            case WEAPON_TYPE.SHOT_GUN:
				EventManagerScript.emit(EventManagerType.SHOT_GUN, this.transform.FindChild("BulletSpawn").gameObject);
                if (m_PlayerScript.m_IsInTurretMode) {
                    m_AudioSource.clip = m_AudioShotgun;
					EventManagerScript.emit(EventManagerType.TOURELLE, m_BulletSpawn.gameObject);
                }
                else {
                    m_AudioSource.clip = m_AudioShotgun;
                }
                break;

        }
        if (m_AudioSource.clip != null) {
            m_AudioSource.Play();
        }

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
