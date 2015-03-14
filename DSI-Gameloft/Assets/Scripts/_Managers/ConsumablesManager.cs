using UnityEngine;
using System.Collections;

public class ConsumablesManager : MonoBehaviour {
    
    #region Singleton
    static private ConsumablesManager s_Instance;
    static public ConsumablesManager instance
    {
        get
        {
            return s_Instance;
        }
    }

    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        DontDestroyOnLoad(this);
        this.Init();
    }
    #endregion

    public bool m_FreezeEnemy;
    public bool m_KillAllEnemy;

    public Collider m_Collider;

    void Init()
    {
        m_FreezeEnemy=false;
        m_KillAllEnemy = false;
    }

    public IEnumerator LetsFreeze()
    {
        m_FreezeEnemy = true;

        m_Collider.enabled = true;

        yield return null;
        yield return null;
        m_Collider.enabled = false;
        m_FreezeEnemy = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LetsFreeze());
        }
    }
}
