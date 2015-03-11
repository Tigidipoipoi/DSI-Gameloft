using UnityEngine;
using System.Collections;

public class EnemyExplosif : Enemy_Script {

    public float m_ExplosionDelay;

    private bool m_IsExplosing;

	// Use this for initialization
    public override void Start()
    {
        base.Start();
    }
	
    void StartExplosionProcess()
    {
        StartCoroutine(blink(m_ExplosionDelay));
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }



	// Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (m_IsAwake == true)
        {
           
        }

        if (m_Life <= 0 && m_IsExplosing==false)
        {
            m_IsExplosing = true;
        }

    }



}
