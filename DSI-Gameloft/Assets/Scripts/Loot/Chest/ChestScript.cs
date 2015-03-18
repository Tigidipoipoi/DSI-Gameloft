using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {
    #region Members
    public GameObject m_ChestLoot;

    private Collider m_ChestCollider;

    public int m_EarnPesos;

    public GameObject m_ObjectParticules;

    private Animation m_Animation;
    AudioSource m_Audio;
    #endregion

    void Start() {
        m_ChestCollider = this.gameObject.GetComponent<Collider>();
        m_Animation = GetComponent<Animation>();
        m_Audio = GetComponent<AudioSource>();

        FloorManager.instance.NewChestAppeared();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (TimerManager.instance.m_RemainingTime > 0) {
                m_Audio.Play();
                m_ChestCollider.enabled = false;
                m_Animation.Play();
                m_ObjectParticules.SetActive(true);

                UIManager.instance.AddComboChest();
                UIManager.instance.UpdatePesos(m_EarnPesos, PesosManager.instance.m_Pesos);

                if (m_ChestLoot != null) {
                    Instantiate(m_ChestLoot, this.transform.position + Random.insideUnitSphere, this.transform.rotation);
                }

                PesosManager.instance.AddPesos(m_EarnPesos);
                ++FloorManager.instance.m_OpenedChestsCount;
                Destroy(this);
            }
        }
    }
}
