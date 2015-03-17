using UnityEngine;
using System.Collections;

public class EnemyStandard : Enemy_Script {
    #region Members
    public IADestination m_Mouvement;

    private bool m_IsReady;

    public float m_DelayBeforeShoot;
    public float m_DelayAfterShoot;
    public GameObject m_PrefabBullet;
    public Transform m_PointForShoot;

    public BulletScript m_BulletScript;
    public float m_BulletPower;
    public float m_BulletSpeed;

    Animator m_Animator;
    #endregion

    public override void Start() {
        base.Start();
        m_IsReady = true;

        m_Animator = this.GetComponent<Animator>();
    }

    IEnumerator WaitAndShoot() {
        if (m_IsAwake == true) {
            //m_Animator.SetTrigger("Aiming");
            yield return new WaitForSeconds(m_DelayBeforeShoot);

            if (m_Mouvement.m_IsAtDistance == false) {
                StopCoroutine(WaitAndShoot());
            }

            GameObject bullet = Instantiate(m_PrefabBullet, m_PointForShoot.position, this.transform.rotation) as GameObject;
            m_BulletScript = bullet.GetComponent<BulletScript>();
            m_BulletScript.m_BulletStats.m_Power = m_BulletPower;
            m_BulletScript.m_BulletStats.m_Speed = m_BulletSpeed;
            bullet.layer = LayerMask.NameToLayer("EnemyBullet");

            yield return new WaitForSeconds(m_DelayAfterShoot);
            m_IsReady = true;
        }
    }

    public override void Update() {
        base.Update();

        if (m_Mouvement.m_Destination_Cible == null) {
            m_Mouvement.m_Destination_Cible = m_Player;
        }

        if (m_IsAwake == true) {
            if (m_Mouvement.m_IsAtDistance == true && m_IsReady == true) {
                m_IsReady = false;

                StartCoroutine(WaitAndShoot());
            }
        }

        if (m_IsFreeze == true) {
            m_Mouvement.enabled = false;
        }
        else {
            m_Mouvement.enabled = true;
        }
    }
}
