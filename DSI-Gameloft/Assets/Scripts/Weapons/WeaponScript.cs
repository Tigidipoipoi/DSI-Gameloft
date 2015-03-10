using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
    #region Members
    public Weapon m_WeaponStats;
    public GameObject m_BulletPrefab;
    public Transform m_BulletSpawn;
    int m_BulletLayer;
    #endregion

    void Start () {
        m_BulletLayer = LayerMask.NameToLayer (m_WeaponStats.m_IsAlly
            ? "AllyBullet"
            : "EnemyBullet");
    }

    public IEnumerator AutoFire () {
        while (true) {
            this.Fire ();
            yield return new WaitForSeconds (m_WeaponStats.m_FireRate);
        }
    }

    public void Fire () {
        GameObject bulletGO = Object.Instantiate (m_BulletPrefab, m_BulletSpawn.position, m_BulletSpawn.rotation) as GameObject;
        bulletGO.layer = m_BulletLayer;
    }
}
