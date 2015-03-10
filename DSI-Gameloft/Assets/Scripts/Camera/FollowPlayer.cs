using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    #region Members
    public Transform mPlayerTrans;
    Vector3 mDistanceToPlayer;
    #endregion

    void Start () {
        mDistanceToPlayer = new Vector3 (0.0f, 8.0f, -6.0f);
    }

    void Update () {
        Vector3 newCamPos = mPlayerTrans.transform.position;
        newCamPos += mDistanceToPlayer;
        this.transform.position = newCamPos;
        this.transform.LookAt (mPlayerTrans.position);
    }
}
