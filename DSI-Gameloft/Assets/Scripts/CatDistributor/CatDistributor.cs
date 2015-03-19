using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatDistributor : MonoBehaviour {
    #region Members
    public List<GameObject> ChatListe = new List<GameObject>();
    public List<AudioClip> MiaouSound = new List<AudioClip>();

    public Transform gauche;
    public Transform droit;

    private int m_RescuedCatCount = 100;
    #endregion

    void Start() {
        PlayerPrefs.SetInt("RescuedCats", 43);
        m_RescuedCatCount = PlayerPrefs.GetInt("RescuedCats", 43);

        if (m_RescuedCatCount > 0) {
            StartCoroutine(LootCat());
        }
    }

    IEnumerator LootCat() {
        for (int i = 0; i < m_RescuedCatCount; i++) {
            Instantiate(ChatListe[Random.Range(0, ChatListe.Count)], this.transform.position, Random.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
