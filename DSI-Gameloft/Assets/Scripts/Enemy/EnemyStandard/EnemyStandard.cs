﻿using UnityEngine;
using System.Collections;

public class EnemyStandard : Enemy_Script
{

    public IADestination m_Mouvement;

    private bool m_IsReady;

    public float m_DelayBeforeShoot;

    public GameObject m_PrefabBullet;
    public Transform m_PointForShoot;

    public BulletScript m_BulletScript;
    public float m_BulletPower;
    public float m_BulletSpeed;

	// Use this for initialization
	public override void Start () 
    {
        base.Start();
        m_IsReady = true;

        m_BulletScript = m_PrefabBullet.GetComponent<BulletScript>();
        m_BulletScript.m_BulletStats.m_Power = m_BulletPower;
        m_BulletScript.m_BulletStats.m_Speed = m_BulletSpeed;
	}
	
    IEnumerator WaitAndShoot()
    {
        if (m_IsAwake == true)
        {
            yield return new WaitForSeconds(m_DelayBeforeShoot);

            if(m_Mouvement.m_IsAtDistance == false)
            {
                StopCoroutine(WaitAndShoot());
            }

            GameObject bullet = Instantiate(m_PrefabBullet, m_PointForShoot.position, this.transform.rotation) as GameObject ;
            bullet.layer = LayerMask.NameToLayer("EnemyBullet");
            m_IsReady = true;
        }
    }

	// Update is called once per frame
	public override void Update () 
    {
        base.Update();

        if (m_Mouvement.m_Destination_Cible == null)
        {
            m_Mouvement.m_Destination_Cible = m_Player;
        }

        if (m_IsAwake == true)
        {
            if (m_Mouvement.m_IsAtDistance == true && m_IsReady == true)
            {
                m_IsReady = false;

                StartCoroutine(WaitAndShoot());

            }
        }
	}
}