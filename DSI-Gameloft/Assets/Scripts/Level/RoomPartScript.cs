using UnityEngine;
using System.Collections;

public class RoomPartScript : MonoBehaviour {
    #region Members
    // PlayTest ONLY
    public int m_RPIndex = -1;

    public RoomScript m_ParentRoom;
    public bool m_IsReachable;
    public int m_EnemyCount;
    public bool m_HasDoor;
    public Utils.DIRECTIONS m_DoorPosition;
    public RoomScript.ROOM_POSITION m_RoomPosition;
    public SceneryScript.SCENERY_TYPE m_WallType;

    GameObject m_ChildContent;

    public const float c_PartWidth = 80.0f;
    public const float c_PartHeight = 50.0f;

    GameObject m_WallCollider;
    #endregion

    void Awake() {
        m_WallCollider = Resources.Load<GameObject>("Prefabs/Wall/WallCollider");
    }

    public void AttachContent(GameObject prefab) {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; ++i) {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        m_ChildContent = Object.Instantiate(prefab,
            this.transform.position, Quaternion.identity) as GameObject; ;
        m_ChildContent.name = "Content";
        m_ChildContent.transform.parent = this.transform;
    }

    public void AttachContent(GameObject wall, bool isWall) {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; ++i) {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        GameObject wallCollider = Object.Instantiate(m_WallCollider,
                this.transform.position, Quaternion.identity) as GameObject;

        wallCollider.name = "Collider";
        wallCollider.transform.parent = this.transform;

        m_ChildContent = wall;
        m_ChildContent.name = "Content";
        m_ChildContent.transform.parent = this.transform;
    }
}
