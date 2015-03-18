using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextCptChat : MonoBehaviour {

	// Temp
	Text m_ChatText;

	// Use this for initialization
	void Start () {
		int chat = PlayerPrefs.GetInt("RescuedCats", 0);
		m_ChatText = GetComponent<Text>();
		m_ChatText.text = string.Format("{0}", chat.ToString("0"));
	}

}
