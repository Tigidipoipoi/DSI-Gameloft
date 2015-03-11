using UnityEngine;
using System.Collections;

public class EnemyMissile : Enemy_Script
{

    public IADestination m_Mouvement;

    private bool m_IsReady;

    public float m_DelayBeforeShoot;

    public GameObject m_PrefabBullet;
    public Transform m_PointForShoot;

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
