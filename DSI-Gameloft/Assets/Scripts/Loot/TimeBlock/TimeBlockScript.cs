using UnityEngine;
using System.Collections;

public class TimeBlockScript : MonoBehaviour {

    public float m_MagnetizeDistance;
    private float m_DistanceFromPlayer;
    private Rigidbody m_Rigidbody;
    private float m_Speed;
    private Vector3 m_Direction;
    public Transform m_Player;

    private float m_EcartDistance;

    private PlayerScript m_PlayerScript;

    public float m_GainTime;

    AudioSource m_AudioSource;
    bool m_IsCatch;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_IsCatch = false;
    }

	// Update is called once per frame
	void Update () 
    {
	    if(m_Player!=null)
        {
            
            m_EcartDistance=  Vector3.Distance(this.transform.position, m_Player.position);

            if (m_EcartDistance < m_MagnetizeDistance)
            {
                m_Direction = (m_Player.position - this.gameObject.transform.position).normalized;

                m_Speed = Mathf.Abs((m_MagnetizeDistance - m_EcartDistance))*4;
                m_Rigidbody.velocity = m_Direction * m_Speed;
            }
        }

        if (!m_AudioSource.isPlaying && m_IsCatch==true)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            m_AudioSource.Play();
            TimerManager.instance.AddTime(m_GainTime);
            m_IsCatch = true;
            Destroy(this.gameObject);
        }
    }

}
