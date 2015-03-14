using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatDistributor : MonoBehaviour {

    public List<GameObject> ChatListe = new List<GameObject>();

    public Transform gauche;
    public Transform droit;

    private int chats=100;

	// Use this for initialization
	void Start () {

        //chats = PlayerPrefs.GetInt("Chats", 0);

        if(chats>0)
        {
            StartCoroutine(LootCat());
            
        }
	}

    IEnumerator LootCat ()
    {

        for (int i = 0; i < chats; i++)
        {
            Instantiate(ChatListe[Random.Range(0, ChatListe.Count)], new Vector3(Random.Range(gauche.position.x, droit.position.x), gauche.position.y, gauche.position.z), Random.rotation);
            yield return new WaitForSeconds(0.1f);
        }

    }

}
