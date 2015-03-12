using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    #region Members
    public Rigidbody m_Rigidbody;
    public Bullet m_BulletStats;
    public Renderer m_Renderer;

    public int m_EnemyBulletLayer;
    public int m_EnemyLayer;
    public int m_AllyBulletLayer;
    public int m_GroundLayer;
    #endregion

    public virtual void Start () {
        m_Rigidbody.velocity = this.transform.forward * m_BulletStats.m_Speed;
        m_Renderer = this.GetComponent<Renderer> ();
        m_EnemyBulletLayer = LayerMask.NameToLayer ("EnemyBullet");
        m_EnemyLayer = LayerMask.NameToLayer ("Enemy");
        m_AllyBulletLayer = LayerMask.NameToLayer ("AllyBullet");
        m_GroundLayer = LayerMask.NameToLayer ("Ground");
    }

    public virtual void GetDamage () {
        Destroy (this.gameObject);
    }

    public virtual void Update () {
        if (!m_Renderer.IsVisibleFrom (Camera.main)) {
            PreDestroy ();
        }
    }

    public virtual void OnCollisionEnter (Collision other) {
        GameObject otherGO = other.gameObject;

        if (otherGO.layer != this.gameObject.layer) {
            if (this.gameObject.layer == m_EnemyBulletLayer) {
                // ToDo: Handle timer
                if (otherGO.tag == "Player") {
                    TimerManager.instance.LoseTime (m_BulletStats.m_Power);
                    Debug.Log ("Player shot !");
                }
            }

            PreDestroy ();
        }
    }

    public void PreDestroy () {
        GameObject.Destroy (this.gameObject);
    }
}
