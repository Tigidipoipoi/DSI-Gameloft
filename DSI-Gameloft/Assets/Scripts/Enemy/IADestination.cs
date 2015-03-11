using UnityEngine;
using System.Collections;

public class IADestination : MonoBehaviour {

    private Rigidbody m_Rigidbody;

    public Transform m_Destination_Cible;
    Vector3 m_Direction;
    [SerializeField]
    private float m_Speed;
    public float m_SpeedMax;
    public float m_Acceleration;

    public float m_BreakDistance;
    public bool m_IsAtDistance;
    public bool m_RotateAroundPlayer;

    private bool m_ChangeRotate;
    public float m_RotateSpeed;

    void Start()
    {
        m_Rigidbody = this.gameObject.GetComponent<Rigidbody>();

        if(m_Acceleration==0)
        {
            m_Acceleration = 0.1f;
            Debug.LogWarning("Pas d'acceleration mise. Acceleration tweaké automatiquement à 0.1");
        }
        if(m_SpeedMax==0)
        {
            Debug.LogWarning("Pas de speedmax tweaké!");
        }
        if (m_ChangeRotate==true && m_RotateSpeed<=0)
        {
            Debug.LogWarning("Pas de RotateSpeed tweaké");
            if (m_SpeedMax == 0)
            {
                Debug.LogWarning("Pas de speedmax tweaké!");
            }
                if (m_SpeedMax > 0)
            {
                Debug.LogWarning("Mise en place à la vitesse de speedmax");
            }
           
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Destination_Cible != null)
        {
            if (Vector3.Distance(this.transform.position, m_Destination_Cible.position) <= m_BreakDistance)
            {
                m_IsAtDistance = true;
                m_Rigidbody.velocity = Vector3.zero;
                if (m_RotateAroundPlayer == true)
                {
                    if (m_ChangeRotate)
                    {
                        transform.RotateAround(m_Destination_Cible.position, Vector3.up, m_RotateSpeed * Time.deltaTime);
                    }
                    else
                    {
                        transform.RotateAround(m_Destination_Cible.position, Vector3.up, (m_RotateSpeed * Time.deltaTime) * -1);
                    }

                }
            }
            else
            {
                m_IsAtDistance = false;
                m_Direction = (m_Destination_Cible.position - this.gameObject.transform.position).normalized;

                if (m_Speed < m_SpeedMax)
                {
                    m_Speed += m_Acceleration;
                }
                if (m_Speed > m_SpeedMax)
                {
                    m_Speed = m_SpeedMax;
                }
                m_Rigidbody.velocity = m_Direction * m_Speed;
            }
        }

    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer==9)
        {
            m_ChangeRotate = !m_ChangeRotate;
        }
    }
    
}
