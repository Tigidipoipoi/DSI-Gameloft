using UnityEngine;
using System.Collections;

public class AnimationComboChest : MonoBehaviour {

    Animator m_Anim;

    void Start()
    {
        m_Anim = GetComponent<Animator>();
    }

	public void StartCombo(bool IsCombo)
    {
        m_Anim.SetBool("Combo", IsCombo);
        
    }

}
