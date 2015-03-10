using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    #region Members
    public Transform m_PlayerTrans;
    Vector3 m_DistanceToPlayer;
    #endregion

    void Start () {
        m_DistanceToPlayer = new Vector3 (0.0f, 8.0f, -6.0f);
    }

    void Update () {
        Vector3 newCamPos = m_PlayerTrans.transform.position;
        newCamPos += m_DistanceToPlayer;
        this.transform.position = newCamPos;
        this.transform.LookAt (m_PlayerTrans.position);
    }
}
