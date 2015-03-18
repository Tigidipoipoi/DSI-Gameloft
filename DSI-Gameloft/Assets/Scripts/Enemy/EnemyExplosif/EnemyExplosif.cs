using UnityEngine;
using System.Collections;

public class EnemyExplosif : Enemy_Script {

    #region Members
    public float m_ExplosionDelay;

    private bool m_IsExplosing;

    public IADestination m_Mouvement;

    public float m_RadiusExplosion;
    public float m_ExplosionDamages;
    #endregion

    public override void Start() {
        base.Start();
    }

    IEnumerator ExplosionProcess() {
        m_Mouvement.enabled = false;
        StartCoroutine(blink(m_ExplosionDelay));
        yield return new WaitForSeconds(m_ExplosionDelay);
        ExplosionDamage(this.transform.position, m_RadiusExplosion);
    }

    void ExplosionDamage(Vector3 center, float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].tag == "Player") {
                TimerManager.instance.LoseTime(m_ExplosionDamages);
            }
            if (hitColliders[i].gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                Enemy_Script m_EnemyScript;
                m_EnemyScript = (Enemy_Script)hitColliders[i].GetComponent(typeof(Enemy_Script));
                m_EnemyScript.GetDamage(m_ExplosionDamages);

            }
            if (hitColliders[i].gameObject.layer == LayerMask.NameToLayer("EnemyBullet")) {
                BulletScript m_BulletScript;
                m_BulletScript = (BulletScript)hitColliders[i].GetComponent(typeof(Enemy_Script));
                m_BulletScript.GetDamage();
            }
        }
    }

    public override void Update() {
        base.Update();

        if (m_Mouvement.m_Destination_Cible == null) {
            m_Mouvement.m_Destination_Cible = m_Player;
        }

        if (m_IsAwake) {
            if (m_Mouvement.m_IsAtDistance == true && m_IsExplosing == false) {
                m_IsExplosing = true;
                StartCoroutine(ExplosionProcess());
            }
        }

        if (m_IsFreeze) {
            m_Mouvement.enabled = false;
        }
        else {
            m_Mouvement.enabled = true;
        }
    }
}
