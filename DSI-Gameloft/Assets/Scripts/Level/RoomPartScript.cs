﻿using UnityEngine;
using System.Collections;

public class RoomPartScript : MonoBehaviour {
    #region Members
    public RoomScript m_ParentRoom;
    public bool m_IsReachable;
    public int m_EnemyCount;
    public bool m_HasDoor;
    public Utils.DIRECTIONS m_DoorPosition;
    public RoomScript.ROOM_POSITION m_RoomPosition;
    public SceneryScript.SCENERY_TYPE m_WallType;

    GameObject m_ChildContent;

    public const float c_PartWidth = 40.0f;
    public const float c_PartHeight = 25.0f;
    #endregion

    public void AttachContent (GameObject prefab) {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; ++i) {
            Destroy (this.transform.GetChild (i).gameObject);
        }

        m_ChildContent = Object.Instantiate (prefab,
            this.transform.position, Quaternion.identity) as GameObject; ;
        m_ChildContent.name = "Content";
        m_ChildContent.transform.parent = this.transform;
    }

    public void AttachContent (GameObject wall, bool isWall) {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; ++i) {
            Destroy (this.transform.GetChild (i).gameObject);
        }

        m_ChildContent = wall;
        m_ChildContent.name = "Content";
        m_ChildContent.transform.parent = this.transform;
    }
}
