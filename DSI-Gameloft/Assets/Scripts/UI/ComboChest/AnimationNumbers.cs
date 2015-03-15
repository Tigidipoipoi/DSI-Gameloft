using UnityEngine;
using System.Collections;

public class AnimationNumbers : MonoBehaviour {
    Animator m_Anim;

    void Start()
    {
        m_Anim = GetComponent<Animator>();
    }

    public void StartCombo(int nbr)
    {
        m_Anim.SetInteger("Combo", nbr);
    }
}
