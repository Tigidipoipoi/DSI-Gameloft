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
    }

    public void GetDamage(float m_Damage)
    {
        StartCoroutine(blink());
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

    public IEnumerator blink(float time = 3)
    {
        float delay = 0.15f;
        Color renderer_memory = renderer.material.color;
        while (time > 0)
        {

            if (renderer.material.color == renderer_memory)
            {
                renderer.material.color = Color.red;
            }
            else
            {
                renderer.material.color = renderer_memory;
            }

            yield return new WaitForSeconds(delay);

            time -= delay;
        }

        renderer.material.color = renderer_memory;
        yield return null;
    }


}
