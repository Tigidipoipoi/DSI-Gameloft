using UnityEngine;
using System.Collections;

public class IARandomMove : MonoBehaviour {

    private Rigidbody m_Rigidbody;

    bool m_IsCurrentRandomMove;


    //PAUSE/////////////////////////////////////////////////////////

    private bool m_IsPauseRandom;

    [SerializeField]
    private float m_PauseTime;
    public float m_PauseMinTime;
    public float m_PauseMaxTime;

    //MOVE//////////////////////////////////////////////////////////

    public float m_DistanceDeplacement;
    Vector3 m_Destination;
    Vector3 m_Direction;

    private float m_Speed;
    public float m_SpeedMax;

    void Start()
    {
        m_Rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        
        StartCoroutine(CurrentPause());
        
    }

    IEnumerator CurrentPause()
    {
      
       if(m_PauseMaxTime<m_PauseMinTime)
       {
           m_PauseMaxTime = m_PauseMinTime;
           Debug.LogWarning("Attention, PauseMaXTime==PauseMinTime");
       }

       if (m_PauseMinTime == m_PauseMaxTime)
       {
           m_IsPauseRandom = true;
       }

      if(m_IsPauseRandom==false)
      {
          m_PauseTime = Random.Range(m_PauseMinTime, m_PauseMaxTime);
      }
      else
      {
          m_PauseTime = m_PauseMinTime;
      }

      yield return new WaitForSeconds(m_PauseTime);
      //Préparation au mouvement
      m_Destination = this.gameObject.transform.position +(Random.insideUnitSphere * m_DistanceDeplacement);
      m_Destination.y = 1.0f;
      Debug.Log(m_Destination);
      m_Direction = (m_Destination-this.gameObject.transform.position).normalized;
      Debug.Log(m_Direction);

      m_IsCurrentRandomMove=true;
    }


	
	// Update is called once per frame
	void Update () 
    {
	    if(m_IsCurrentRandomMove==true)
        {
             m_Rigidbody.velocity= m_Direction * m_SpeedMax;
           
            if(Vector3.Distance(this.transform.position,m_Destination)<=0.25f)
            {
                
                m_Rigidbody.velocity = Vector3.zero;
                m_IsCurrentRandomMove = false;
                StartCoroutine(CurrentPause());
                
            }
        }
	}
    
}
