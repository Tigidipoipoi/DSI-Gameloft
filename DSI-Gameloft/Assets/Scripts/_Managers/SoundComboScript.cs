using UnityEngine;
using System.Collections;

public class SoundComboScript : MonoBehaviour {

	public AudioClip[] ComboSound = new AudioClip[9];

	AudioSource m_AudioSource;

	// Use this for initialization
	void Start () {
		m_AudioSource = GetComponent<AudioSource>();
	}
	
	public void LetsSound(int combo)
	{
		m_AudioSource.clip = ComboSound[combo];

		if(m_AudioSource.isPlaying)
		{
			m_AudioSource.Stop();
		}

		m_AudioSource.Play();

	}

}
