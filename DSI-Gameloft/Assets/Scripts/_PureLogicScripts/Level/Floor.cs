using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Floor {
    #region Members
    public List<Room> m_Rooms;
    public int m_RoomCount;
    #endregion

    public void GenerateLevel () {
    }
}
