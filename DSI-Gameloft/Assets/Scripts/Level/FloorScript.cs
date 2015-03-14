using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class FloorScript : MonoBehaviour {
    #region Members
    // Room parts
    GameObject[] m_Rooms;
    GameObject[] m_RoomPrefabs;
    Transform m_RoomsContainer;
    public int m_RoomCount;

    // Constants
    public const int c_RoomPrefabCount = 1;

    // Doors
    public DoorScript[] m_Doors;

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
        // TO DO
    }
}
