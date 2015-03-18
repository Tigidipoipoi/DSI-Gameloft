using UnityEngine;
using System.Collections;

public class UIKeyScript : MonoBehaviour {

    Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }



    public void AnimKey()
    {
        m_Animator.SetTrigger("GetKey");
    }

}
