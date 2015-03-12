using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    #region Members
    public Transform m_PlayerTrans;
    Vector3 m_DistanceToPlayer;
    #endregion

    void Start () {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
        m_DistanceToPlayer = this.transform.position - playerPosition;
    }

    void Update () {
        Vector3 newCamPos = m_PlayerTrans.transform.position;
        newCamPos += m_DistanceToPlayer;
        this.transform.position = newCamPos;
        this.transform.LookAt (m_PlayerTrans.position);
    }
}
