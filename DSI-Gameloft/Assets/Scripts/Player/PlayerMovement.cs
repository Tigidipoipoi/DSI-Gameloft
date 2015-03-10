using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    #region Members
    public float mLerpEase = 0.5f;
    public float mMaxSpeed = 8.0f;
    public float mAcceleration = 1.0f;
    public float mDeceleration = 1.0f;
    float mCurrentSpeed = 0.0f;
    public float mPseudoEpsilon = 0.01f;

    int mGroundLayerMask;
    Vector3 mTargetPosition;
    NavMeshAgent mNavMeshAgent;
    #endregion

    void Start () {
        mGroundLayerMask = LayerMask.GetMask ("Ground");
        mNavMeshAgent = this.GetComponent<NavMeshAgent> ();
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast (ray, out hit, Mathf.Infinity, mGroundLayerMask)) {
                mTargetPosition = hit.point;
                mTargetPosition.y = 1.083333f;
                mNavMeshAgent.SetDestination (mTargetPosition);
            }
        }
    }
}
