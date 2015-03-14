using UnityEngine;
using System.Collections;

public class SceneryScript : MonoBehaviour {
    public enum SCENERY_TYPE {
        NONE = 0,
        HORIZONTAL,
        VERTICAL,
        CORNER,
        DOOR_VER,
        DOOR_HOR,

        COUNT
    }

    #region Members
    public RoomScript m_ParentRoom;
    public SCENERY_TYPE m_Type;

    public const float c_PartWidth = 40.0f;
    public const float c_PartHeight = 25.0f;

    GameObject m_ChildContent;

    GameObject m_WallCollider;
    #endregion

    void Awake () {
        m_WallCollider = Resources.Load<GameObject> ("Prefabs/Wall/WallCollider");
    }

    public void AttachContent (GameObject wall) {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; ++i) {
            Destroy (this.transform.GetChild (i).gameObject);
        }

        GameObject wallCollider = Object.Instantiate (m_WallCollider,
            this.transform.position, Quaternion.identity) as GameObject;

        wallCollider.name = "Collider";
        wallCollider.transform.parent = this.transform;

        m_ChildContent = wall;
        m_ChildContent.name = "Content";
        m_ChildContent.transform.parent = this.transform;
    }
}
