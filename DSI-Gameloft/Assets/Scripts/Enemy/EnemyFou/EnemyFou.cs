using UnityEngine;
using System.Collections;

public class EnemyFou : Enemy_Script
{
    public IADestination m_Mouvement;

    private bool m_IsReady;

    public float m_ShootCooldown;

    public float m_DelayBeforeShoot;
    public float m_DelayAfterShoot;

    public GameObject m_PrefabBullet;
    public Transform m_PointForShoot;

    private GameObject bullet;

	// Use this for initialization
	public override void Start () 
    {
        base.Start();
        m_IsReady = true;
	}
	
    IEnumerator WaitAndShoot()
    {
        yield return new WaitForSeconds(m_ShootCooldown);

        if (m_IsAwake == true)
        {
            yield return new WaitForSeconds(m_DelayBeforeShoot);
            m_Mouvement.enabled = false;
            bullet = Instantiate(m_PrefabBullet, m_PointForShoot.position, this.transform.rotation) as GameObject ;
            bullet.layer = LayerMask.NameToLayer("EnemyBullet");

        }

        while(bullet!=null)
        {
            m_IsReady = false;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(m_DelayAfterShoot);

        m_IsReady = true;
        m_Mouvement.enabled = true;

        yield return null;
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
