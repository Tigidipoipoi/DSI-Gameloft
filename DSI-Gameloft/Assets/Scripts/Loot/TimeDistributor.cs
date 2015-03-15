using UnityEngine;
using System.Collections;

public class TimeDistributor : MonoBehaviour {

    public float m_EarnTime;

    public float m_ValeurBigTime;
    public float m_ValeurMedTime;
    public float m_ValeurSmallTime;

    private int m_BigTime;
    private int m_MedTime;
    private int m_SmallTime;

    public GameObject BigTime;
    public GameObject MedTime;
    public GameObject SmallTime;

    private TimeBlockScript m_TimeBlockScript;


    // Use this for initialization
	void Start () 
    {
        Debug.Log(m_EarnTime);
        if (TimerManager.instance.m_RemainingTime > 0)
        {
            m_TimeBlockScript = BigTime.GetComponent<TimeBlockScript>();
            m_TimeBlockScript.m_GainTime = m_ValeurBigTime;

            m_TimeBlockScript = MedTime.GetComponent<TimeBlockScript>();
            m_TimeBlockScript.m_GainTime = m_ValeurMedTime;

            m_TimeBlockScript = SmallTime.GetComponent<TimeBlockScript>();
            m_TimeBlockScript.m_GainTime = m_ValeurSmallTime;

            m_BigTime=0;
            m_MedTime=0;
            m_SmallTime=0;

       
            if (m_EarnTime >= (m_ValeurSmallTime * m_ValeurMedTime + (m_ValeurSmallTime * m_ValeurBigTime) + (m_ValeurSmallTime * (m_ValeurBigTime + m_ValeurMedTime))))
            {
                m_BigTime = (int)m_EarnTime / (int)m_ValeurBigTime;
                m_MedTime = (int)((m_EarnTime % m_ValeurBigTime) / m_ValeurMedTime);
                m_SmallTime = (int)(((m_EarnTime % m_ValeurBigTime) % m_ValeurMedTime) / m_ValeurSmallTime);
            }
            else
            {

                if (m_EarnTime >= m_ValeurSmallTime * m_ValeurMedTime)
                {
                    m_MedTime = (int)m_EarnTime / (int)m_ValeurMedTime;
                    m_SmallTime = (int)((((int)m_EarnTime % (int)m_ValeurMedTime)) / m_ValeurSmallTime);
                }
                else
                {
                    m_SmallTime = (int)m_EarnTime / (int)m_ValeurSmallTime;
                }
            }

            

            StartCoroutine(Distribution());
        }
	}
	
	IEnumerator Distribution()
    {
        while (m_SmallTime > 0)
        {
            Instantiate(SmallTime, this.transform.position + Random.insideUnitSphere, this.transform.rotation);
            m_SmallTime--;
            yield return new WaitForSeconds(0.1f);

        }
        while (m_MedTime > 0)
        {
            Instantiate(MedTime, this.transform.position + Random.insideUnitSphere, this.transform.rotation);
            m_MedTime--;
            yield return new WaitForSeconds(0.1f);
        }
        while (m_BigTime > 0)
        {
            Instantiate(BigTime, this.transform.position + Random.insideUnitSphere, this.transform.rotation);
            m_BigTime--;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
        Destroy(this.gameObject);
    }
}
