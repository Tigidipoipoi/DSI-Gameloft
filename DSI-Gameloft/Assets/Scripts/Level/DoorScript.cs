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

    Vector3 m_EnteringVelocity;
    Vector3 m_NormAxis;

    const float c_EnteringRoomOffset = 5.0f;
    #endregion

    void Start() {
        switch (m_DoorPos) {
            case DOOR_POSITION.CENTER_HOR:
            case DOOR_POSITION.LEFT:
            case DOOR_POSITION.RIGHT:
                m_NormAxis = Vector3.forward;
                break;
            case DOOR_POSITION.CENTER_VER:
            case DOOR_POSITION.DOWN:
            case DOOR_POSITION.UP:
                m_NormAxis = Vector3.right;
                break;
        }
    }

    public void AttachContent(GameObject door) {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; ++i) {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        m_ChildContent = door;
        m_ChildContent.name = "Content";
        m_ChildContent.transform.parent = this.transform;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            //m_EnteringVelocity = other.GetComponent<Rigidbody>().velocity;

            this.StartCoroutine("DoorTransition");

            //Vector2 roomToLoadIndex = m_Room1Index == FloorManager.instance.m_CurrentRoomIndex
            //    ? m_Room2Index
            //    : m_Room1Index;

            //FloorManager.instance.LoadRoam(roomToLoadIndex);
        }
    }

    IEnumerator DoorTransition() {
        PlayerMovement playerMoveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMoveScript.FreezePosition(disableInputs: true);

        Vector2 roomToLoadIndex = m_Room1Index == FloorManager.instance.m_CurrentRoomIndex
            ? m_Room2Index
            : m_Room1Index;
        Vector2 roomToUnloadIndex = m_Room1Index == FloorManager.instance.m_CurrentRoomIndex
            ? m_Room1Index
            : m_Room2Index;

        FloorManager.instance.LoadRoam(roomToLoadIndex);

        // ToDo: display transition screen
        yield return new WaitForSeconds(0.1f);
        // ToDo: hide transition screen

        Vector3 warpPos = playerMoveScript.transform.position;

        // Right => Left
        if ((int)roomToLoadIndex.x > (int)roomToUnloadIndex.x) {
            warpPos.x += SceneryScript.c_PartWidth + c_EnteringRoomOffset;
        }
        // Left => Right
        else if ((int)roomToLoadIndex.x < (int)roomToUnloadIndex.x) {
            warpPos.x -= SceneryScript.c_PartWidth + c_EnteringRoomOffset;
        }
        // Up => Down
        else if ((int)roomToLoadIndex.y > (int)roomToUnloadIndex.y) {
            warpPos.z -= SceneryScript.c_PartHeight + c_EnteringRoomOffset;
        }
        // Down => Up
        else if ((int)roomToLoadIndex.y < (int)roomToUnloadIndex.y) {
            warpPos.z += SceneryScript.c_PartHeight + c_EnteringRoomOffset;
        }

        playerMoveScript.transform.position = warpPos;

        FloorManager.instance.UnloadRoam(roomToUnloadIndex);
        FloorManager.instance.m_CurrentRoomIndex = roomToLoadIndex;

        playerMoveScript.enabled = true;
    }

    void OnTriggerExit(Collider other) {
        //if (other.tag == "Player") {
        //    Vector3 enterVelFormated = this.FormatVelocityToAxis(m_EnteringVelocity.normalized, m_NormAxis);
        //    Vector3 exitVelFormated = this.FormatVelocityToAxis(other.GetComponent<Rigidbody>().velocity.normalized, m_NormAxis);

        //    float velsAngle = Vector3.Angle(enterVelFormated, exitVelFormated);

        //    // Same side
        //    if (velsAngle >= 90.0f) {
        //        Vector2 roomToUnloadIndex = m_Room1Index == FloorManager.instance.m_CurrentRoomIndex
        //            ? m_Room2Index
        //            : m_Room1Index;

        //        FloorManager.instance.LoadRoam(roomToUnloadIndex);
        //    }
        //    else {
        //        Vector2 roomToUnloadIndex = m_Room1Index == FloorManager.instance.m_CurrentRoomIndex
        //            ? m_Room1Index
        //            : m_Room2Index;
        //        Vector2 newRoomIndex = roomToUnloadIndex == m_Room2Index
        //            ? m_Room1Index
        //            : m_Room2Index;

        //        FloorManager.instance.UnloadRoam(roomToUnloadIndex);
        //        FloorManager.instance.m_CurrentRoomIndex = newRoomIndex;
        //    }
        //}
    }

    Vector3 FormatVelocityToAxis(Vector3 velocity, Vector3 axis) {
        velocity.Normalize();
        velocity.x = velocity.x * axis.x;
        velocity.y = velocity.y * axis.y;
        velocity.z = velocity.z * axis.z;

        return velocity.normalized;
    }
}
