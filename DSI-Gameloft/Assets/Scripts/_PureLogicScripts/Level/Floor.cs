using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Floor {
    #region Members
    public List<Room> m_Rooms;
    public int m_RoomCount;
    #endregion

    public void GenerateLevel (int roomCount/*, int maxRow, int maxCol*/) {
        m_RoomCount = roomCount;
        //int maxRoomCount = maxRow * maxCol;
        //if (m_RoomCount > maxRoomCount) {
        //    m_RoomCount = maxRoomCount;
        //}

        m_Rooms = new List<Room> ();
        for (int i = 0; i < m_RoomCount; ++i) {
            m_Rooms[i] = new Room ();
        }
    }
}
