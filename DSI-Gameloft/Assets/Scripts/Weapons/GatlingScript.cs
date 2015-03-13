using UnityEngine;
using System.Collections;

public class GatlingScript : WeaponScript {
    #region Members
    public GameObject m_PierceBulletPrefab;
    public float m_HoldPower;
    public float m_FireRate2;
    public float m_FireRate3;
    #endregion

    public override IEnumerator AutoFire () {
        while (true) {
            if (m_PlayerScript.m_IsInTurretMode) {
                yield return new WaitForEndOfFrame ();
            }
            else {
                yield return new WaitForSeconds (m_WeaponStats.m_FireRate);
            }

            this.Fire ();
        }
    }

    public override void Fire () {
        GameObject bulletGO = Object.Instantiate (m_PierceBulletPrefab,
            m_BulletSpawn.position, m_BulletSpawn.rotation) as GameObject;
        bulletGO.layer = m_BulletLayer;
        bulletGO.GetComponent<PiercingBulletScript> ().m_BulletStats.m_Power = m_PlayerScript.m_IsInTurretMode
            ? m_HoldPower : m_WeaponStats.m_Power;
    }

    public override void LevelUpWeapon () {
        base.LevelUpWeapon ();
        if (m_WeaponStats.m_CurrentLevel == 1) {
            // TEMP
            m_WeaponStats.m_FireRate *= m_FireRate2;
        }
        else if (m_WeaponStats.m_CurrentLevel == 2) {
            // TEMP
            m_WeaponStats.m_FireRate *= m_FireRate3;
        }
    }
}
