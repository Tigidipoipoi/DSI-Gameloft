using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

    private bool m_ChestOpen;

    public GameObject m_ChestLoot;

    private Collider m_ChestCollider;

	// Use this for initialization
	void Start () {
        m_ChestOpen = false;
        m_ChestCollider = this.gameObject.GetComponent<Collider>();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            m_ChestOpen = true;
            m_ChestCollider.enabled = false;
            Instantiate(m_ChestLoot, this.transform.position + Random.insideUnitSphere, this.transform.rotation);
            Destroy(this);
        }
    }
}
