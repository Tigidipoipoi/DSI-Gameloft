using UnityEngine;
using System.Collections;

public class HomingBulletScript : BulletScript {
    #region Members
    public Transform m_Target;
    #endregion

    public override void Start () {
        // TEST ONLY!!!
        this.transform.Rotate (Vector3.right, 90.0f);
    }

    public virtual void Update () {
        m_Rigidbody.velocity = (m_Target.position - this.transform.position).normalized * m_BulletStats.m_Speed;
    }
}
