using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour {
    public enum DOOR_ORIENTATION {
        HORIZONTAL = 0,
        VERTICAL,

        COUNT
    }

    #region Members
    public Vector2 m_RoomEnterIndex;
    public RoomScript.ROOM_POSITION m_EnterPos;

    public Vector2 m_RoomExitIndex;
    public RoomScript.ROOM_POSITION m_ExitPos;

    public DOOR_ORIENTATION m_Orientation;
    #endregion
}
