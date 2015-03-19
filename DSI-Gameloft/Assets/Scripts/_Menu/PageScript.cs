using UnityEngine;
using System.Collections;

public class PageScript : MonoBehaviour
{
    Animator m_anim;

    void Start()
    {
        m_anim = GetComponent<Animator>();
    }
    public void Change(int transition)
    {
        m_anim.SetInteger("valeur", transition);
    }
}