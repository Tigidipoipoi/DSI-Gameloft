using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class FloorScript : MonoBehaviour {
    #region Members
    // Seeding
    public bool m_IsSeed;

    // Room parts
    [HideInInspector]
    public GameObject[] m_Rooms;
    GameObject[] m_RoomPrefabs;
    Transform m_RoomsContainer;
    public int m_RoomCount;

    // Constants
    public const int c_RoomPrefabCount = 1;
    //public const int c_HorDoorPrefabCount = 1;
    //public const int c_VerDoorPrefabCount = 1;

    // Doors
    DoorScript[] m_Doors;
    public GameObject m_HorDoorPrefabs;
    public GameObject m_VerDoorPrefabs;
    Transform m_DoorContainer;

    public Vector2 m_FloorSize;
    #endregion

    void Awake () {
        m_RoomPrefabs = new GameObject[c_RoomPrefabCount];
        for (int i = 0; i < c_RoomPrefabCount; ++i) {
            m_RoomPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Rooms/Room{0}", i));
        }
    }

    void Start () {
        List<GameObject> childGOList = new List<GameObject> ();

        // Rooms
        m_RoomsContainer = this.transform.FindChild ("Rooms");
        for (int i = 0; i < m_RoomCount; ++i) {
            RoomScript childRScript = m_RoomsContainer.GetChild (i).GetComponent<RoomScript> ();

            childGOList.Add (childRScript.gameObject);
        }
        m_Rooms = childGOList.ToArray ();

        // Doors
        m_DoorContainer = this.transform.FindChild ("Doors");
        int doorCount = m_DoorContainer.childCount;
        List<DoorScript> childDSList = new List<DoorScript> ();
        for (int i = 0; i < doorCount; ++i) {
            DoorScript childDScript = m_DoorContainer.GetChild (i).GetComponent<DoorScript> ();

            childDSList.Add (childDScript);
        }
        m_Doors = childDSList.ToArray ();

        this.GenerateDoors ();

        if (m_IsSeed) {
            FloorManager.instance.LoadSeed(this);
        }
    }

    void GenerateDoors () {
        int doorsCount = m_Doors.Length;
        for (int i = 0; i < doorsCount; ++i) {
            Vector3 doorPosition = m_Doors[i].transform.position;
            GameObject newDoorGO = this.BuildDoor (doorPosition, m_Doors[i].m_DoorPos);

            m_Doors[i].AttachContent (newDoorGO);
        }
    }

    GameObject BuildDoor (Vector3 newDoorPos, DoorScript.DOOR_POSITION doorPos) {
        GameObject prefab = null;
        switch (doorPos) {
            case DoorScript.DOOR_POSITION.LEFT:
            case DoorScript.DOOR_POSITION.CENTER_HOR:
            case DoorScript.DOOR_POSITION.RIGHT:
                prefab = m_HorDoorPrefabs;
                break;
            case DoorScript.DOOR_POSITION.UP:
            case DoorScript.DOOR_POSITION.CENTER_VER:
            case DoorScript.DOOR_POSITION.DOWN:
                prefab = m_VerDoorPrefabs;
                break;

            default:
                Debug.LogError ("FloorScript::BuildDoor=> Wrong wall type!");
                break;
        }

        GameObject newDoor = Object.Instantiate (prefab,
                newDoorPos, Quaternion.identity) as GameObject;

        return newDoor;
    }
}
