using UnityEngine;
using System.Collections;

public class CircleTouchScript : MonoBehaviour {
    #region Members
    Animator m_Animator;
    #endregion

    void Start() {
        m_Animator = this.GetComponent<Animator>();
    }

    public void PlayAnim(Vector3 newPos) {
        newPos.y = 1.26f;
        this.transform.position = newPos;
        m_Animator.SetTrigger("PlayCibleTouch");
    }
}
