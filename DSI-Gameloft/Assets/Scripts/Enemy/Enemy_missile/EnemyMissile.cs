using UnityEngine;
using System.Collections;

public class EnemyMissile : Enemy
{

    public IADestination m_Mouvement;

    private bool m_IsReady;

    public float m_DelayBeforeShoot;

    public GameObject m_PrefabBullet;
    public Transform m_PointForShoot;

	// Use this for initialization
	void Start () 
    {
        m_IsReady = true;
	}
	
    IEnumerator WaitAndShoot()
    {
        if (m_IsAwake == true)
        {
            yield return new WaitForSeconds(m_DelayBeforeShoot);

            Instantiate(m_PrefabBullet, m_PointForShoot.position, Quaternion.identity);

            m_IsReady = true;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (m_IsAwake == true)
        {
            if (m_Mouvement.m_IsAtDistance == true && m_IsReady == true)
            {
                m_IsReady = false;

                StartCoroutine(WaitAndShoot());

            }
        }

        if (m_Life <= 0)
        {
            Destroy(this.gameObject);
        }

	}
}
