using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour {
    public enum DOOR_POSITION {
        UP = 0,
        CENTER_VER,
        DOWN,
        LEFT,
        CENTER_HOR,
        RIGHT,

        COUNT
    }

    #region Members
    public Vector2 m_Room1Index;
    public Vector2 m_Room2Index;

    public DOOR_POSITION m_DoorPos;

    GameObject m_ChildContent;
    #endregion

    public void AttachContent (GameObject door) {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; ++i) {
            Destroy (this.transform.GetChild (i).gameObject);
        }

        m_ChildContent = door;
        m_ChildContent.name = "Content";
        m_ChildContent.transform.parent = this.transform;
    }
}
