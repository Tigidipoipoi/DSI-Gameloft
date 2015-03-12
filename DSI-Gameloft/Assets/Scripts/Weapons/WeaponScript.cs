using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
    #region Members
    public Weapon m_WeaponStats;
    public GameObject m_BulletPrefab;
    public GameObject m_HomingBulletPrefab;
    public Transform m_BulletSpawn;
    public GameObject m_Holder;

    int m_BulletLayer;
    PlayerScript m_PlayerScript;
    #endregion

    void Start () {
        m_BulletLayer = LayerMask.NameToLayer (m_WeaponStats.m_IsAlly
            ? "AllyBullet"
            : "EnemyBullet");
        m_PlayerScript = m_Holder.GetComponent<PlayerScript> ();
    }

    public IEnumerator AutoFire () {
        while (true) {
            this.Fire ();
            yield return new WaitForSeconds (m_WeaponStats.m_FireRate);
        }
    }

    public void Fire () {
        GameObject bulletGO = Object.Instantiate (m_WeaponStats.m_IsHoming ? m_HomingBulletPrefab : m_BulletPrefab,
            m_BulletSpawn.position, m_BulletSpawn.rotation) as GameObject;
        bulletGO.layer = m_BulletLayer;
        bulletGO.GetComponent<BulletScript> ().m_BulletStats.m_Power = m_WeaponStats.m_Power;

        if (m_WeaponStats.m_IsHoming) {
            if (m_Holder.tag == "Player") {
                bulletGO.GetComponent<HomingBulletScript> ().m_Target = m_PlayerScript.m_EnemyTarget;
            }
            else {
                bulletGO.GetComponent<HomingBulletScript> ().m_Target = m_PlayerScript.transform;
            }
        }

    }
}
