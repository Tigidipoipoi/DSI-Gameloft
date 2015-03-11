using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    #region Members
    public float m_TimeBeforeTurrelMode = 0.2f;
    public int m_MoveSpeed;
    public float m_BreakDistance;

    float m_LastClickDownTime;
    int m_EnemyLayerMask;
    int m_GroundLayerMask;
    Vector3 m_TargetPosition;
    Rigidbody m_Rigidbody;
    IEnumerator m_MoveToTarget;
    PlayerScript m_PlayerScript;
    #endregion

    void Start () {
        m_EnemyLayerMask = LayerMask.GetMask ("Enemy");
        m_GroundLayerMask = LayerMask.GetMask ("Ground");
        m_PlayerScript = this.GetComponent<PlayerScript> ();
        m_Rigidbody = this.GetComponent<Rigidbody> ();
        m_MoveToTarget = this.MoveToTarget ();
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
                this.StopCoroutine (m_MoveToTarget);
                m_TargetPosition = hit.point;
                m_TargetPosition.y = 1.0f;
                m_MoveToTarget = this.MoveToTarget ();
                this.StartCoroutine (m_MoveToTarget);
            }
        }
        // Focus Fire
        else if (Input.GetMouseButton (0)
            && Mathf.Abs (Time.time - m_LastClickDownTime) > m_TimeBeforeTurrelMode) {
            m_PlayerScript.Unlock ();
        }
    }

    IEnumerator MoveToTarget () {
        while (Vector3.Distance (this.transform.position, m_TargetPosition) > m_BreakDistance) {
            m_Rigidbody.velocity = (m_TargetPosition - this.transform.position).normalized * m_MoveSpeed;
            yield return null;
        }

        m_Rigidbody.velocity = Vector3.zero;
    }
}
