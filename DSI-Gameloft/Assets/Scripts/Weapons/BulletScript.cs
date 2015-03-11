using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    #region Members
    public Rigidbody m_Rigidbody;
    public Bullet m_BulletStats;
    public Renderer m_Renderer;
    #endregion

    public virtual void Start () {
        m_Rigidbody.velocity = this.transform.forward * m_BulletStats.m_Speed;
        m_Renderer = this.GetComponent<Renderer> ();
    }

    public virtual void Update () {
        if (!m_Renderer.IsVisibleFrom (Camera.main)) {
            PreDestroy ();
        }
    }

    public virtual void OnCollisionEnter (Collision other) {
        GameObject GO = other.gameObject;

        if (GO.layer != this.gameObject.layer) {
            PreDestroy ();
        }
    }

    public void PreDestroy () {
        GameObject.Destroy (this.gameObject);
    }
}
