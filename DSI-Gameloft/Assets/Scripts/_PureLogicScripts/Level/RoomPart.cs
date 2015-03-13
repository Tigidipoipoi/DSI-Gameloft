using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RoomPart {
    public enum ROOM_PART_TYPE {
        SCENERY = 0,
        WALKABLE,
        DOOR,

        COUNT
    }

    #region Members
    public ROOM_PART_TYPE m_Type;
    #endregion
}
