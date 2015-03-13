using UnityEngine;
using System.Collections;

public class Enemy_Script : MonoBehaviour {

    public float m_Life;

    public bool m_IsAwake;

    public Transform m_Player;

    Renderer renderer;

    public GameObject m_EnnemyMissile2;

    private Color m_StandardColor;


    public float m_EarnedTime;
    public GameObject m_TimeDistributor;
    private TimeDistributor m_TimeDistributorScript;

    public virtual void Start()
    {
        renderer=this.GetComponent<Renderer>();
        m_StandardColor= renderer.material.color;

        m_TimeDistributorScript = m_TimeDistributor.GetComponent<TimeDistributor>();
        m_TimeDistributorScript.m_EarnTime = m_EarnedTime;

    }

    public void GetDamage(float m_Damage)
    {
        StartCoroutine(blink());
        m_Life -= m_Damage;
        if(m_Life<=0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        if (name == "EnemyMissile" && m_EnnemyMissile2!=null)
        {
            Instantiate(m_EnnemyMissile2, this.transform.position, this.transform.rotation);
            Instantiate(m_EnnemyMissile2, this.transform.position, this.transform.rotation);
        }

        m_Player.GetComponent<PlayerScript>().Unlock();


        Instantiate(m_TimeDistributor, this.transform.position, this.transform.rotation);

        Destroy(this.gameObject);
    }

    public virtual void Update()
    {

        if(renderer.IsVisibleFrom(Camera.main))
        {
            m_IsAwake = true;
            m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            m_IsAwake = false;
        }

        transform.LookAt(m_Player, Vector3.up);


    }

    public IEnumerator blink(float time = 0.5f)
    {
        float delay = 0.15f;
        
        while (time > 0)
        {

            if (renderer.material.color == m_StandardColor)
            {
                renderer.material.color = Color.red;
            }
            else
            {
                renderer.material.color = m_StandardColor;
            }

            yield return new WaitForSeconds(delay);

            time -= delay;
        }

        renderer.material.color = m_StandardColor;
        yield return null;
    }


}
