using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Room {
    #region Members
    public List<RoomPart> m_RoomParts;
    public int m_WalkableRoomPartCount;
    public const int c_MaxWaklableRoomParts = 9;
    public bool m_IsReachable;
    #endregion

    public Room ()
        : this (false) { }

    public Room (bool isReachable) {
        m_IsReachable = isReachable;
    }

    public void GenerateRoom () {
        if (m_WalkableRoomPartCount > c_MaxWaklableRoomParts) {
            m_WalkableRoomPartCount = c_MaxWaklableRoomParts;
        }

        m_RoomParts = new List<RoomPart> ();
    }
}
