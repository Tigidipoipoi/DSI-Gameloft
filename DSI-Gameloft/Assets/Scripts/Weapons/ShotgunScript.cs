using UnityEngine;
using System.Collections;

public class ShotgunScript : WeaponScript {
    #region Members
    public int m_BallsPerShotLV0;
    public int m_BallsPerShotLV1;
    public int m_BallsPerShotLV2;

    public float m_LockRange;
    public float m_HoldRange;

    // %2 == 1
    int m_CurrentBPS;
    float m_CurrentRange;
    #endregion

    protected override void Start () {
        base.Start ();
        m_CurrentBPS = m_BallsPerShotLV0;
        m_CurrentRange = m_LockRange;
    }

    public override void Fire () {
        m_CurrentRange = m_PlayerScript.m_IsInTurretMode
            ? m_HoldRange
            : m_LockRange;

        GameObject bulletGO = Object.Instantiate (m_BulletPrefab,
            m_BulletSpawn.position, m_BulletSpawn.rotation) as GameObject;
        bulletGO.layer = m_BulletLayer;
        bulletGO.GetComponent<BulletScript> ().m_BulletStats.m_Power = m_WeaponStats.m_Power;

        // Left
        this.Spread (-1);
        // Right
        this.Spread (1);
    }

    public override void LevelUpWeapon () {
        base.LevelUpWeapon ();

        if (m_WeaponStats.m_CurrentLevel == 1) {
            m_CurrentBPS = m_BallsPerShotLV1;

        }
        else if (m_WeaponStats.m_CurrentLevel == 2) {
            m_CurrentBPS = m_BallsPerShotLV2;
        }
    }

    public void Spread (int coef) {
        int halfBPS = (m_CurrentBPS - 1) / 2;
        float spaceBetweenBullets = m_CurrentRange / halfBPS;
        float halfRange = m_CurrentRange * 0.5f;

        Quaternion oldSpawnRotation = m_BulletSpawn.rotation;

        m_BulletSpawn.Rotate (Vector3.up, coef * halfRange);
        for (int i = 0; i < halfBPS; ++i) {
            Quaternion oldTurnedSpawnRotation = m_BulletSpawn.rotation;

            m_BulletSpawn.Rotate (Vector3.up, -coef * i * spaceBetweenBullets);
            Object.Instantiate (m_BulletPrefab, m_BulletSpawn.position, m_BulletSpawn.rotation);

            m_BulletSpawn.rotation = oldTurnedSpawnRotation;
        }
        m_BulletSpawn.rotation = oldSpawnRotation;
    }
}
