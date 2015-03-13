using UnityEngine;
using System.Collections;

public class RoomPartScript : MonoBehaviour {
    #region Members
    public RoomScript m_ParentRoom;
    public bool m_IsReachable;
    public int m_EnemyCount;
    public bool m_HasDoor;
    public Utils.DIRECTIONS m_DoorPosition;

    public const float c_PartWidth = 40.0f;
    public const float c_PartHeight = 25.0f;
    #endregion
}
