﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    #region Members
    public Rigidbody m_Rigidbody;
    public Bullet m_BulletStats;
    public Renderer m_Renderer;
    private Enemy_Script m_EnemyScript;

    public int m_EnemyBulletLayer;
    public int m_EnemyLayer;
    public int m_AllyBulletLayer;
    public int m_GroundLayer;
    #endregion

    public virtual void Start() {
        m_Rigidbody.velocity = this.transform.forward * m_BulletStats.m_Speed;
        m_Renderer = this.GetComponent<Renderer>();
        m_EnemyBulletLayer = LayerMask.NameToLayer("EnemyBullet");
        m_EnemyLayer = LayerMask.NameToLayer("Enemy");
        m_AllyBulletLayer = LayerMask.NameToLayer("AllyBullet");
        m_GroundLayer = LayerMask.NameToLayer("Ground");
    }

    public virtual void GetDamage() {
        Destroy(this.gameObject);
    }

    public virtual void Update() {
        if (!m_Renderer.IsVisibleFrom(Camera.main)) {
            PreDestroy();
        }
    }

    public virtual void OnCollisionEnter(Collision other) {
        GameObject otherGO = other.gameObject;

        if (otherGO.layer != this.gameObject.layer) {
            bool isEnemyBullet = this.gameObject.layer == m_EnemyBulletLayer;

            if (isEnemyBullet) {
                if (otherGO.tag == "Player") {
                    TimerManager.instance.LoseTime(m_BulletStats.m_Power);
                }
            }

            if (this.gameObject.layer == m_AllyBulletLayer) {
                if (otherGO.layer == m_EnemyLayer) {
                    m_EnemyScript = otherGO.GetComponent<Enemy_Script>();
                    m_EnemyScript.GetDamage(m_BulletStats.m_Power);

                }
            }

            if (isEnemyBullet) 
			{
                if (otherGO.layer != m_EnemyLayer) {
                    PreDestroy();
                }
            }
            else 
			{
                PreDestroy();
            }
        }
    }

    public void PreDestroy() {
		if(this.gameObject.tag=="Bombe")
		{
			EventManagerScript.emit(EventManagerType.EXPLOSION, this.gameObject);
			GameObject.Destroy(this.gameObject, 0.7f);
		}
		else
		{
			GameObject.Destroy(this.gameObject);
		}
		

        
    }
}
