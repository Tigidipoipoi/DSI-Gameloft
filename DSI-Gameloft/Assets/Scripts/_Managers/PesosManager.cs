using UnityEngine;
using System.Collections;

public class PesosManager : MonoBehaviour {
    #region Singleton
    static private PesosManager s_Instance;
    static public PesosManager instance {
        get {
            return s_Instance;
        }
    }

    void Awake() {
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
        this.Init();
    }
    #endregion

    #region members

    public int m_Pesos;

    #endregion

    public void Init() {
        m_Pesos = 0;
    }

    public void AddPesos(int pesos) {
        m_Pesos += pesos;
    }

    public void SavePesos() {
        PlayerPrefs.SetInt("Pesos", PlayerPrefs.GetInt("Pesos", 0) + m_Pesos);
    }

}
