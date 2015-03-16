using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour {
    #region Singleton
    static private FloorManager s_Instance;
    static public FloorManager instance {
        get {
            return s_Instance;
        }
    }

    void Awake() {
        if (s_Instance == null)
            s_Instance = this;
        //DontDestroyOnLoad(this);
        //this.Init();
    }
    #endregion

    #region Members
    public int m_RemainingEnemiesCount;
    public GameObject[] m_FloorPrefabs;
    public FloorScript m_CurrentFloor;
    public bool m_KeyPoped;
    public bool m_KeyAquired;
    public bool m_HasLoadedSeed;
    public UIKeyScript KeyScript;

    public Vector2 m_CurrentRoomIndex;
    #endregion

    public void Init() {
        m_KeyPoped = false;
        m_KeyAquired = false;
        Transform playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        Transform startTrans = m_CurrentFloor.transform.Find("Start");
        playerTrans.position = startTrans.position;

        RoomScript[] roomScripts = m_CurrentFloor.m_Rooms.Select(x => x.GetComponent<RoomScript>()).ToArray();
        m_CurrentRoomIndex = roomScripts.FirstOrDefault(x => x.m_IsStartRoom).m_FloorIndex;
        int roomCount = roomScripts.Length;
        for (int i = 0; i < roomCount; ++i) {
            if (roomScripts[i].m_FloorIndex != m_CurrentRoomIndex) {
                roomScripts[i].gameObject.SetActive(false);
            }
        }
    }


    public void GetKey() {
        m_KeyAquired = true;
        KeyScript.AnimKey();
    }

    public bool MustPopKey() {
        --m_RemainingEnemiesCount;
        if (m_KeyPoped
            || m_KeyAquired) {
            return false;
        }

        int popRate = 0;
        if (m_RemainingEnemiesCount < 10) {
            popRate = 110 - 10 * m_RemainingEnemiesCount;
        }
        else {
            popRate = 100 / m_RemainingEnemiesCount;
        }
        int rng = Random.Range(0, 99);

        bool mustPopKey = popRate > rng;
        if (mustPopKey) {
            m_KeyPoped = true;
        }

        return mustPopKey;
    }

    public void NewEnemyAppeared() {
        ++m_RemainingEnemiesCount;
    }

    public void LoadSeed(FloorScript seed) {
        m_CurrentFloor = seed;
        m_HasLoadedSeed = true;

        this.Init();
    }

    public void LoadRoam(Vector2 roomIndex) {
        RoomScript[] roomScripts = m_CurrentFloor.m_Rooms.Select(x => x.GetComponent<RoomScript>()).ToArray();
        GameObject roomToLoadGO = roomScripts.FirstOrDefault(x => x.m_FloorIndex == roomIndex).gameObject;

        roomToLoadGO.SetActive(true);
    }

    public void UnloadRoam(Vector2 roomIndex) {
        RoomScript[] roomScripts = m_CurrentFloor.m_Rooms.Select(x => x.GetComponent<RoomScript>()).ToArray();
        GameObject roomToLoadGO = roomScripts.FirstOrDefault(x => x.m_FloorIndex == roomIndex).gameObject;

        roomToLoadGO.SetActive(false);
    }
}
