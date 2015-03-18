using UnityEngine;
using System.Collections;

public class CollectibleWeaponScript : MonoBehaviour {
    #region Members
    WeaponScript m_WeaponScript;
    PlayerScript m_PlayerScript;

    public float m_MagnetizeDistance;
    private Rigidbody m_Rigidbody;
    private float m_Speed;
    private Vector3 m_Direction;

    private float m_EcartDistance;
    #endregion

    void Start() {
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        m_WeaponScript = this.transform.parent.GetComponent<WeaponScript>();
        m_Rigidbody = m_WeaponScript.gameObject.GetComponent<Rigidbody>();
    }

    void Update() {
        if (m_PlayerScript != null) {
            m_EcartDistance = Vector3.Distance(this.transform.position, m_PlayerScript.transform.position);

            if (m_EcartDistance < m_MagnetizeDistance) {
                m_Direction = (m_PlayerScript.transform.position - this.gameObject.transform.position).normalized;

                m_Speed = Mathf.Abs((m_MagnetizeDistance - m_EcartDistance)) * 4;
                m_Rigidbody.velocity = m_Direction * m_Speed;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            m_PlayerScript.LootWeapon(m_WeaponScript);

            Destroy(m_Rigidbody);
            Destroy(this.gameObject);
        }
    }
}
