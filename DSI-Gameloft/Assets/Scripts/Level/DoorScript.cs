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
            m_EnteringVelocity = other.GetComponent<Rigidbody>().velocity;

            Vector2 roomToLoadIndex = m_Room1Index == FloorManager.instance.m_CurrentRoomIndex
                ? m_Room2Index
                : m_Room1Index;

            FloorManager.instance.LoadRoam(roomToLoadIndex);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Transition end!");
            Vector3 enterVelFormated = this.FormatVelocityToAxis(m_EnteringVelocity.normalized, m_NormAxis);
            Vector3 exitVelFormated = this.FormatVelocityToAxis(other.GetComponent<Rigidbody>().velocity.normalized, m_NormAxis);

            float velsAngle = Vector3.Angle(enterVelFormated, exitVelFormated);

            // Same side
            if (velsAngle >= 90.0f) {
                Vector2 roomToUnloadIndex = m_Room1Index == FloorManager.instance.m_CurrentRoomIndex
                    ? m_Room2Index
                    : m_Room1Index;

                FloorManager.instance.LoadRoam(roomToUnloadIndex);
            }
            else {
                Vector2 roomToUnloadIndex = m_Room1Index == FloorManager.instance.m_CurrentRoomIndex
                    ? m_Room1Index
                    : m_Room2Index;
                Vector2 newRoomIndex = roomToUnloadIndex == m_Room2Index
                    ? m_Room1Index
                    : m_Room2Index;

                FloorManager.instance.UnloadRoam(roomToUnloadIndex);
                FloorManager.instance.m_CurrentRoomIndex = newRoomIndex;
            }
        }
    }

    Vector3 FormatVelocityToAxis(Vector3 velocity, Vector3 axis) {
        velocity.Normalize();
        velocity.x = velocity.x * axis.x;
        velocity.y = velocity.y * axis.y;
        velocity.z = velocity.z * axis.z;

        return velocity.normalized;
    }
}
