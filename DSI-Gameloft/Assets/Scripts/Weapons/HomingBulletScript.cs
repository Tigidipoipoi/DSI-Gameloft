using UnityEngine;
using System.Collections;

public class HomingBulletScript : BulletScript {
    #region Members
    public Transform m_Target;
    #endregion

    public override void Start () {
        base.Start();
        m_Rigidbody.velocity = Vector3.zero;

    }

    public override void Update () {
        if (m_Target != null)
        {
            m_Rigidbody.velocity = (m_Target.position - this.transform.position).normalized * m_BulletStats.m_Speed;
        }

        if(m_Target==null)
        {
            Destroy(this.gameObject);
        }

        base.Update ();
    }
}
