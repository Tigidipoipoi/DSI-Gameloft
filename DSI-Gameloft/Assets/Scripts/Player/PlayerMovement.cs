using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    #region Members
    public float m_TimeBeforeTurretMode = 0.2f;
    public float m_MoveSpeed = 7.0f;
    public float m_BreakDistance = 0.1f;
    public CircleTouchScript m_CircleTouchScript;
    public Animator m_UpAnimator;
    public Animator m_DownAnimator;

    float m_LastClickDownTime;
    int m_EnemyOrObstacleLayerMask;
    int m_ClickableLayerMask;
    int m_MoveObstrusiveLayerMask;

    float m_ColliderRadius;

    Vector3 m_TargetPosition;
    Rigidbody m_Rigidbody;
    IEnumerator m_MoveToTarget;
    PlayerScript m_PlayerScript;

    public Transform m_BulletSpawn;
    #endregion

    void Start() {
        m_EnemyOrObstacleLayerMask = LayerMask.GetMask("Enemy", "Obstacle");
        m_ClickableLayerMask = LayerMask.GetMask("Ground", "Wall", "Door");
        m_MoveObstrusiveLayerMask = LayerMask.GetMask("Ground", "Wall", "Obstacle");
        m_PlayerScript = this.GetComponent<PlayerScript>();
        m_PlayerScript.UpdateWeaponsHoming(isHoming: true);
        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_MoveToTarget = this.MoveToTarget();

        m_ColliderRadius = this.GetComponent<CapsuleCollider>().radius;
    }

    void Update() {
        if (!m_PlayerScript.m_IsInTurretMode
            && Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            m_LastClickDownTime = Time.time;

            // Lock
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_EnemyOrObstacleLayerMask)) {
                m_PlayerScript.LockTarget(hit.collider.transform.GetComponent<EnemyLock>());
            }
            // Move
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_ClickableLayerMask)) {
                this.StopCoroutine(m_MoveToTarget);
                Vector3 targetPos = hit.point;
                targetPos.y = m_PlayerScript.c_PlayerPosYClamp;

                #region Check walls on the way
                Vector3 rayOrigin = this.transform.position;
                Vector3 estimatedWay = targetPos - this.transform.position;
                rayOrigin.y = m_PlayerScript.c_PlayerPosYClamp;
                Ray rayCM = new Ray(rayOrigin, estimatedWay);

                if (Physics.Raycast(rayCM, out hit, Vector3.Distance(rayOrigin, targetPos), m_MoveObstrusiveLayerMask)) {
                    estimatedWay = hit.point - this.transform.position;
                    estimatedWay -= estimatedWay.normalized * m_ColliderRadius;
                    targetPos = estimatedWay + this.transform.position;
                    targetPos.y = m_PlayerScript.c_PlayerPosYClamp;
                }
                #endregion

                m_TargetPosition = targetPos;

                #region FeedBack
                m_CircleTouchScript.PlayAnim(targetPos);
                #endregion

                m_MoveToTarget = this.MoveToTarget();
                this.StartCoroutine(m_MoveToTarget);
            }
        }
        // Turret Mode
        else if (Input.GetMouseButton(0)
            && Mathf.Abs(Time.time - m_LastClickDownTime) > m_TimeBeforeTurretMode
            && !m_PlayerScript.m_IsInTurretMode
            //&& m_PlayerScript.m_EnemyTarget != null
            ) {
            m_DownAnimator.SetBool("IsWalking", false);
            m_UpAnimator.SetBool("IsWalking", false);
            this.FreezePosition();

            m_UpAnimator.SetBool("IsAiming", true);
            m_PlayerScript.m_IsInTurretMode = true;
            m_PlayerScript.Unlock();
            m_PlayerScript.UpdateWeaponsHoming(isHoming: false);
            m_PlayerScript.StartCoroutine("TurretShoot");

        }
        // End of Turret Mode
        else if (Input.GetMouseButtonUp(0)) {
            if (m_PlayerScript.m_IsInTurretMode) {
                m_UpAnimator.SetBool("IsAiming", false);
                m_PlayerScript.m_IsInTurretMode = false;
                m_PlayerScript.UpdateWeaponsHoming(isHoming: true);
            }
        }
    }

    public void FreezePosition(bool disableInputs = false) {
        this.StopCoroutine(m_MoveToTarget);
        m_Rigidbody.velocity = Vector3.zero;
        m_DownAnimator.SetBool("IsWalking", false);
        m_UpAnimator.SetBool("IsWalking", false);

        if (disableInputs) {
            this.enabled = false;
        }
    }

    IEnumerator MoveToTarget() {
        m_DownAnimator.SetBool("IsWalking", true);
        m_UpAnimator.SetBool("IsWalking", true);

        Vector3 currentPos = this.transform.position;
        currentPos.y = 0.0f;
        Vector3 targetPos = m_TargetPosition;
        targetPos.y = 0.0f;
        while (Vector3.Distance(currentPos, targetPos) > m_BreakDistance) {
            if (m_PlayerScript.m_EnemyTarget == null) {
                Vector3 lookAtTarget = m_TargetPosition;
                lookAtTarget.y = m_PlayerScript.c_PlayerPosYClamp;
                m_PlayerScript.m_DownBodyTrans.LookAt(lookAtTarget);

                if (m_PlayerScript.m_EnemyTarget == null) {
                    m_PlayerScript.m_UpBodyTrans.LookAt(lookAtTarget);
                }
            }

            m_Rigidbody.velocity = (targetPos - currentPos).normalized * m_MoveSpeed;
            yield return null;
            currentPos = this.transform.position;
            currentPos.y = 0.0f;
            targetPos = m_TargetPosition;
            targetPos.y = 0.0f;
        }

        m_Rigidbody.velocity = Vector3.zero;
        m_DownAnimator.SetBool("IsWalking", false);
        m_UpAnimator.SetBool("IsWalking", false);
    }
}
