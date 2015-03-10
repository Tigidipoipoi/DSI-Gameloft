using UnityEngine;
using System.Collections;

public class IAPatrouille : MonoBehaviour
{

    private Rigidbody m_Rigidbody;

    

    public Transform[] m_SpotPoints;
    public int m_StepSpotPoints;
    private Transform m_Destination_Cible;
    public bool m_IsLooping;
    public bool m_IsReverseLooping;
    public bool m_IsAlea;

    Vector3 m_Direction;
    [SerializeField]
    private float m_Speed;
    public float m_SpeedMax;
    public float m_Acceleration;

    public float m_BreakDistance; 

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
        m_StepSpotPoints=0;
        if (m_IsAlea == true)
        {
            m_StepSpotPoints = Random.Range(0, m_SpotPoints.Length);
        }
        m_Destination_Cible = m_SpotPoints[m_StepSpotPoints];

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, m_Destination_Cible.position) <= m_BreakDistance)
        {
            m_Rigidbody.velocity = Vector3.zero;

            if(m_IsAlea==true)
            {
                m_StepSpotPoints = Random.Range(0, m_SpotPoints.Length);
                m_Destination_Cible = m_SpotPoints[m_StepSpotPoints];
            }
            else
            {
                if (m_StepSpotPoints < m_SpotPoints.Length)
                {
                    m_StepSpotPoints++;
                    if (m_StepSpotPoints >= m_SpotPoints.Length)
                    {
                        if (m_IsLooping == true)
                        {
                            m_StepSpotPoints = 0;
                            m_Destination_Cible = m_SpotPoints[m_StepSpotPoints];
                        }
                    }
                    else
                    {
                        m_Destination_Cible = m_SpotPoints[m_StepSpotPoints];
                    }
                }
            }
        }
        else
        {
           m_Direction = (m_Destination_Cible.position - this.gameObject.transform.position).normalized;

           if(m_Speed<m_SpeedMax)
           {
               m_Speed += m_Acceleration;
           }
           if(m_Speed>m_SpeedMax)
           {
               m_Speed = m_SpeedMax;
           }
           m_Rigidbody.velocity = m_Direction * m_Speed;
        }
    }
}
