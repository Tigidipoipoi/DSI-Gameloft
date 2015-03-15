using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    #region Members
    public float m_TimeBeforeTurretMode = 0.1f;
    public float m_MoveSpeed = 10.0f;
    public float m_BreakDistance = 0.1f;

    float m_LastClickDownTime;
    int m_EnemyOrObstacleLayerMask;
    int m_ClickableLayerMask;

    Vector3 m_TargetPosition;
    Rigidbody m_Rigidbody;
    IEnumerator m_MoveToTarget;
    PlayerScript m_PlayerScript;
    #endregion

    void Start () {
        m_EnemyOrObstacleLayerMask = LayerMask.GetMask ("Enemy", "Obstacle");
        m_ClickableLayerMask = LayerMask.GetMask ("Ground", "ClickToMove");
        m_PlayerScript = this.GetComponent<PlayerScript> ();
        m_PlayerScript.UpdateWeaponsHoming (isHoming: true);
        m_Rigidbody = this.GetComponent<Rigidbody> ();
        m_MoveToTarget = this.MoveToTarget ();
    }

    void Update () {
        if (!m_PlayerScript.m_IsInTurretMode
            && Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            m_LastClickDownTime = Time.time;

            // Lock
            if (Physics.Raycast (ray, out hit, Mathf.Infinity, m_EnemyOrObstacleLayerMask)) {
                m_PlayerScript.LockTarget (hit.collider.transform.GetComponent<EnemyLock> ());
            }
            // Move
            else if (Physics.Raycast (ray, out hit, Mathf.Infinity, m_ClickableLayerMask)) {
                this.StopCoroutine (m_MoveToTarget);
                m_TargetPosition = hit.point;
                m_TargetPosition.y = m_PlayerScript.c_PlayerPosYClamp;
                m_MoveToTarget = this.MoveToTarget ();
                this.StartCoroutine (m_MoveToTarget);
            }
        }
        // Turret Mode
        else if (Input.GetMouseButton (0)
            && Mathf.Abs (Time.time - m_LastClickDownTime) > m_TimeBeforeTurretMode
            && !m_PlayerScript.m_IsInTurretMode
            //&& m_PlayerScript.m_EnemyTarget != null
            ) {
            this.FreezePosition ();

            m_PlayerScript.m_IsInTurretMode = true;
            m_PlayerScript.Unlock ();
            m_PlayerScript.UpdateWeaponsHoming (isHoming: false);
            m_PlayerScript.StartCoroutine ("TurretShoot");
        }
        // End of Turret Mode
        else if (Input.GetMouseButtonUp (0)) {
            m_PlayerScript.m_IsInTurretMode = false;
            m_PlayerScript.UpdateWeaponsHoming (isHoming: true);
        }
    }

    void FreezePosition () {
        this.StopCoroutine (m_MoveToTarget);
        m_Rigidbody.velocity = Vector3.zero;
    }

    IEnumerator MoveToTarget () {
        while (Vector3.Distance (this.transform.position, m_TargetPosition) > m_BreakDistance) {
            if (m_PlayerScript.m_EnemyTarget == null) {
                Vector3 lookAtTarget = m_TargetPosition;
                lookAtTarget.y = m_PlayerScript.c_PlayerPosYClamp;
                this.transform.LookAt (lookAtTarget);
            }

            m_Rigidbody.velocity = (m_TargetPosition - this.transform.position).normalized * m_MoveSpeed;
            yield return null;
        }

        m_Rigidbody.velocity = Vector3.zero;
    }
}
