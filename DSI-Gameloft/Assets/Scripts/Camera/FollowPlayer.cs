using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    #region Members
    public Transform m_PlayerTrans;
    Vector3 m_DistanceToPlayer;

    public float magnitude = 1;
    public float duration = 1;
    #endregion

    void Start() {
        m_PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        m_DistanceToPlayer = this.transform.position - m_PlayerTrans.position;
    }

    void Update() {
        Vector3 newCamPos = m_PlayerTrans.transform.position;
        newCamPos += m_DistanceToPlayer;
        this.transform.position = newCamPos;
        this.transform.LookAt(m_PlayerTrans.position);
    }

    public void LetsShake() {
        StopCoroutine("Offset");

        StartCoroutine("Shake");
    }

    IEnumerator Offset() {
        Debug.Log("a");
        while (this.gameObject != null) {
            Vector3 newCamPos = m_PlayerTrans.transform.position;
            newCamPos += m_DistanceToPlayer;
            this.transform.position = newCamPos;
            this.transform.LookAt(m_PlayerTrans.position);
            yield return null;
        }
    }

    IEnumerator Shake() {
        float elapsed = 0.0f;
        Vector3 originalCamPos = this.transform.position;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
            // map value to [-1, 1]
            float x = (Random.value * 2.0f - 1.0f) + this.transform.position.x;
            float y = (Random.value * 2.0f - 1.0f);
            x *= magnitude * damper;
            y *= magnitude * damper;

            this.transform.position = new Vector3(x, originalCamPos.y, originalCamPos.z);

            yield return null;
        }

        StartCoroutine("Offset");
    }
}
