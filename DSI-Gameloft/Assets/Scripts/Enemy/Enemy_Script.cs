using UnityEngine;
using System.Collections;

public class Enemy_Script : MonoBehaviour {
    #region Members
    // Playtest Only !
    public int m_DropTheKey = -1;

    public float m_Life;

    public bool m_IsAwake;

    public Transform m_Player;

    Renderer m_Renderer;

    public GameObject m_EnnemyMissile2;

    private Color m_StandardColor;


    public float m_EarnedTime;
    public GameObject m_TimeDistributor;
    private TimeDistributor m_TimeDistributorScript;
    private GameObject m_KeyPrefab;
    #endregion

    public void Awake () {
        m_KeyPrefab = Resources.Load<GameObject> ("Prefabs/Key/Key");
    }

    private float m_FreezeDelay;
    protected bool m_IsFreeze;

    public virtual void Start () {
        FloorManager.instance.NewEnemyAppeared ();

        m_FreezeDelay = 3;

        m_Renderer = this.GetComponent<Renderer> ();
        m_StandardColor = m_Renderer.material.color;

        m_TimeDistributorScript = m_TimeDistributor.GetComponent<TimeDistributor> ();
        m_TimeDistributorScript.m_EarnTime = m_EarnedTime;

    }

    public void GetDamage (float m_Damage) {
        StartCoroutine (blink ());
        m_Life -= m_Damage;
        if (m_Life <= 0) {
            this.DestroyEnemy ();
        }
    }

    public IEnumerator FreezeEnemy () {
        m_IsFreeze = true;
        yield return new WaitForSeconds (m_FreezeDelay);
        m_IsFreeze = false;
    }



    public void DestroyEnemy () {
        bool mustPopKey = m_DropTheKey > -1
            ? m_DropTheKey > 0
            : FloorManager.instance.MustPopKey ();

        if (name == "EnemyMissile" && m_EnnemyMissile2 != null) {
            Instantiate (m_EnnemyMissile2, this.transform.position, this.transform.rotation);
            Instantiate (m_EnnemyMissile2, this.transform.position, this.transform.rotation);
        }

        m_Player.GetComponent<PlayerScript> ().Unlock ();


        Instantiate (m_TimeDistributor, this.transform.position, this.transform.rotation);

        if (mustPopKey) {
            PopKey ();
        }

        Destroy (this.gameObject);
    }

    public virtual void Update () {

        if (m_Renderer.IsVisibleFrom (Camera.main)) {
            m_IsAwake = true;
            m_Player = GameObject.FindGameObjectWithTag ("Player").transform;
        }
        else {
            m_IsAwake = false;
        }

        transform.LookAt (m_Player, Vector3.up);



    }

    public IEnumerator blink (float time = 0.5f) {
        float delay = 0.15f;

        while (time > 0) {

            if (m_Renderer.material.color == m_StandardColor) {
                m_Renderer.material.color = Color.red;
            }
            else {
                m_Renderer.material.color = m_StandardColor;
            }

            yield return new WaitForSeconds (delay);

            time -= delay;
        }

        m_Renderer.material.color = m_StandardColor;
        yield return null;
    }

    void PopKey () {
        Object.Instantiate (m_KeyPrefab, this.transform.position, Quaternion.identity);
    }
}
