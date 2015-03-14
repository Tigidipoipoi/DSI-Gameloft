using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Miaou : MonoBehaviour {

    public AudioClip[] MiaouSound = new AudioClip[10];
    AudioSource MiaouSource;
    Rigidbody m_Rigidbody;
	// Use this for initialization
	void Start () {

        MiaouSource = GetComponent<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody>();

        StartCoroutine(Miaule());
	}

    IEnumerator Miaule()
    {

        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        MiaouSource.clip = MiaouSound[Random.Range(0, MiaouSound.Length - 1)];
        MiaouSource.Play();

        while (MiaouSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(Bouge());
    }

    IEnumerator Bouge()
    {
        while(true)
        {
            m_Rigidbody.velocity = Vector3.up * Random.Range(5, 10);
            yield return new WaitForSeconds(Random.Range(2, 5f));

        }
    }
}
