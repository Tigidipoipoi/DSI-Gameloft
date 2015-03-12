using UnityEngine;
using System.Collections;

public class CastEnemyScript : MonoBehaviour
{

    public float m_CastDelay;
    public float m_CastDamage;
    public float m_CastDisapear;
    private bool m_Laser;

    private PlayerScript m_PlayerScript;

	// Use this for initialization
	void Start () 
    {
        m_Laser = false;
        StartCoroutine(Cast());
	}
	
   

    IEnumerator Cast()
    {
        yield return new WaitForSeconds(m_CastDelay);
        m_Laser = true;
        Debug.Log("laser");
        
    }
    void Burn(GameObject other)
    {
        m_PlayerScript = (PlayerScript)other.gameObject.GetComponent(typeof(PlayerScript));
        m_PlayerScript.GetDamage(m_CastDamage);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(m_CastDisapear);
        Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if(m_Laser==true)
        {
            m_Laser = false;
            if (other.tag == "Player")
            {
                Debug.Log("touch_player");
                Burn(other.gameObject);
            }
            else
            {
                StartCoroutine(Destroy());
            }
        }

    }

}
