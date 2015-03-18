using UnityEngine;
using System.Collections;

public class OptionAnimation : MonoBehaviour {
    #region Members
    public bool m_Open;

    Animator animator; 
    #endregion

    public void Option() {
        m_Open = !m_Open;
        animator.SetBool("Open", m_Open);
    }
}
