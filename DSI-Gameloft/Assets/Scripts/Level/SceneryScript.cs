using UnityEngine;
using System.Collections;

public class SceneryScript : MonoBehaviour {
    public enum SCENERY_TYPE {
        HORIZONTAL = 0,
        VERTICAL,
        CORNER,
        DOOR,

        COUNT
    }

    #region Members
    public RoomScript m_ParentRoom;
    public SCENERY_TYPE m_Type;

    public const float c_PartWidth = 40.0f;
    public const float c_PartHeight = 25.0f;
    #endregion
}
