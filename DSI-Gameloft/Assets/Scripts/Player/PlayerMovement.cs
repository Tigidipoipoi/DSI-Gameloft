using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    #region Members
    public float m_TimeBeforeTurrelMode = 0.2f;

    float m_LastClickDownTime;
    int m_EnemyLayerMask;
    int m_GroundLayerMask;
    Vector3 m_TargetPosition;
    NavMeshAgent m_NavMeshAgent;
    PlayerScript m_PlayerScript;
    #endregion

    void Start () {
        m_EnemyLayerMask = LayerMask.GetMask ("Enemy");
        m_GroundLayerMask = LayerMask.GetMask ("Ground");
        m_NavMeshAgent = this.GetComponent<NavMeshAgent> ();
        m_PlayerScript = this.GetComponent<PlayerScript> ();
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            m_LastClickDownTime = Time.time;

            // Lock
            if (Physics.Raycast (ray, out hit, Mathf.Infinity, m_EnemyLayerMask)) {
                m_PlayerScript.LockTarget (hit.collider.transform.GetComponent<EnemyLock> ());
            }
            // Move
            else if (Physics.Raycast (ray, out hit, Mathf.Infinity, m_GroundLayerMask)) {
                m_TargetPosition = hit.point;
                m_TargetPosition.y = 1.0f;
                m_NavMeshAgent.SetDestination (m_TargetPosition);
            }
        }
        // Focus Fire
        else if (Input.GetMouseButton (0)
            && Mathf.Abs(Time.time - m_LastClickDownTime) > m_TimeBeforeTurrelMode) {
            m_PlayerScript.Unlock ();
        }
    }
}
