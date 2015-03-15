using UnityEngine;
using System.Collections;

public class EnemyMissile2 : Enemy_Script
{
    public IADestination m_Mouvement;

    private bool m_IsReady;

    public float m_DelayBeforeShoot;

    public GameObject m_PrefabBullet;
    public Transform m_PointForShoot;

    public float m_RangeForShoot;

    public BulletScript m_BulletScript;
    public float m_BulletPower;
    public float m_BulletSpeed;

	// Use this for initialization
	public override void Start () 
    {
        base.Start();
        m_IsReady = true;

        
	}
	
    IEnumerator WaitAndShoot()
    {
        if (m_IsAwake == true)
        {
            yield return new WaitForSeconds(m_DelayBeforeShoot);
            if (m_Mouvement.m_IsAtDistance == false && (Vector3.Distance(this.transform.position, m_Mouvement.m_Destination_Cible.position) <= m_RangeForShoot))
            {
                StopCoroutine(WaitAndShoot());
            }
            GameObject bullet = Instantiate(m_PrefabBullet, m_PointForShoot.position, this.transform.rotation) as GameObject ;
            m_BulletScript = bullet.GetComponent<BulletScript>();
            m_BulletScript.m_BulletStats.m_Power = m_BulletPower;
            m_BulletScript.m_BulletStats.m_Speed = m_BulletSpeed;
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
            if ((m_Mouvement.m_IsAtDistance == true || (Vector3.Distance(this.transform.position,m_Mouvement.m_Destination_Cible.position)<=m_RangeForShoot))&& m_IsReady == true)
            {
                m_IsReady = false;

                StartCoroutine(WaitAndShoot());

            }
        }

        if (m_IsFreeze == true)
        {
            m_Mouvement.enabled = false;
        }
        else
        {
            m_Mouvement.enabled = true;
        }
	}
}
