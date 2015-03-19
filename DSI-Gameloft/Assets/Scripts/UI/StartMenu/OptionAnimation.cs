using UnityEngine;
using System.Collections;

public class OptionAnimation : MonoBehaviour {

    public bool m_Open;

    Animator animator;

    public void Option()
    {
        m_Open = !m_Open;
        animator.SetBool("Open",m_Open);
    }
    
   
}
