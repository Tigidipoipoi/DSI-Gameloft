using UnityEngine;
using System.Collections;

public class AnimationCroix : MonoBehaviour {
    Animator m_Anim;

    void Start()
    {
        m_Anim = GetComponent<Animator>();
    }

    public void StartCombo()
    {
        m_Anim.SetTrigger("Combo");
    }
}
