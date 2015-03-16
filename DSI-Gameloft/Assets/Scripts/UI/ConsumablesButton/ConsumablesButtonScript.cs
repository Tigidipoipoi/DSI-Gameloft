using UnityEngine;
using System.Collections;

public class ConsumablesButtonScript : MonoBehaviour {
    
    AudioSource m_AudioSource;
    public AudioClip m_FreezeClip;
    public AudioClip m_DeathClip;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void DestroyNow(string consumable)
    {
        if (consumable == "freeze")
        {
            if (m_FreezeClip != null)
            {
                m_AudioSource.clip = m_FreezeClip;
                m_AudioSource.Play();
            }
            ConsumablesManager.instance.StartCoroutine("LetsFreeze");
            ShakeManager.instance.LetsShake(1000);
            Handheld.Vibrate();

        }

        if (consumable == "death")
        {
            if (m_DeathClip != null)
            {
                m_AudioSource.clip = m_DeathClip;
                m_AudioSource.Play();
            }
            ConsumablesManager.instance.StartCoroutine("LetsKill");
            ShakeManager.instance.LetsShake(1000);
            Handheld.Vibrate();
        }
            
        Destroy(this.gameObject,2);
    }
}
