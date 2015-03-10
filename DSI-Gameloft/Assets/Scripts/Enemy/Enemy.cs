using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float m_Life;

    protected bool m_IsAwake;

    Renderer renderer;

    void Start()
    {
        renderer.GetComponent<Renderer>();
    }

    void TakeDamage(float m_Damage)
    {

    }

    void Update()
    {

        if(renderer.IsVisibleFrom(Camera.main))
        {
            m_IsAwake = true;
        }
        else
        {
            m_IsAwake = false;
        }



    }


}
