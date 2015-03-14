using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {
    #region Singleton
    static private FloorManager s_Instance;
    static public FloorManager instance {
        get {
            return s_Instance;
        }
    }

    void Awake () {
        if (s_Instance == null)
            s_Instance = this;
        DontDestroyOnLoad (this);
        this.Init ();
    }
    #endregion

    #region Members
    public int m_RoomCount;
    public GameObject[] m_FloorPrefabs;
    public FloorScript m_CurrentFloor;
    #endregion

    public void Init () {
    }


}
