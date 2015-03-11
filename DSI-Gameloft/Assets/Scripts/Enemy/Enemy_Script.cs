using UnityEngine;
using System.Collections;

public class Enemy_Script : MonoBehaviour {

    public float m_Life;

    public bool m_IsAwake;

    public Transform m_Player;

    Renderer renderer;

    public virtual void Start()
    {
        renderer=this.GetComponent<Renderer>();
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void TakeDamage(float m_Damage)
    {
        StartCoroutine(blink());
    }

    public virtual void Update()
    {

        if(renderer.IsVisibleFrom(Camera.main))
        {
            m_IsAwake = true;
        }
        else
        {
            m_IsAwake = false;
        }

        transform.LookAt(m_Player, Vector3.up);


    }

    public IEnumerator blink(float time = 3)
    {
        float delay = 0.15f;

        while (time > 0)
        {

            if (renderer.material.color == Color.white)
            {
                renderer.material.color = Color.red;
            }
            else
            {
                renderer.material.color = Color.white;
            }

            yield return new WaitForSeconds(delay);

            time -= delay;
        }

        renderer.material.color = Color.white;
        yield return null;
    }


}
