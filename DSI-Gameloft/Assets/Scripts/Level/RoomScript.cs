using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RoomScript : MonoBehaviour {
    public enum ROOM_POSITION {
        UP_LEFT = 0,
        UP_CENTER,
        UP_RIGHT,
        LEFT,
        CENTER,
        RIGHT,
        DOWN_LEFT,
        DOWN_CENTER,
        DOWN_RIGHT,

        COUNT
    }

    #region Members
    // Room parts
    RoomPartScript[] m_RoomParts;
    GameObject[] m_RoomPartPrefabs;
    Transform m_RoomPartsContainer;

    // Walls
    GameObject[] m_RoomWalls;
    GameObject m_WallHorPrefabs;
    GameObject m_WallVerPrefabs;
    GameObject m_WallCornerPrefabs;
    Transform m_WallsContainer;

    // Constants
    public const int c_RoomPartPrefabCount = 11;
    public const int c_WallHorPrefabCount = 1;
    public const int c_WallVerPrefabCount = 1;
    public const int c_WallCornerPrefabCount = 1;

    // Doors
    public Utils.DIRECTIONS[] m_PossibleDoorPositions;

    // Floor
    public Vector2 m_FloorIndex;
    public bool m_IsStartRoom;
    #endregion

    void Awake() {
        m_RoomPartPrefabs = new GameObject[c_RoomPartPrefabCount];
        for (int i = 0; i < c_RoomPartPrefabCount; ++i) {
            m_RoomPartPrefabs[i] = Resources.Load<GameObject>(string.Format("Prefabs/RoomParts/RoomPart{0}", i));
        }

        m_WallHorPrefabs = Resources.Load<GameObject>("Prefabs/Wall/WallH");
        m_WallVerPrefabs = Resources.Load<GameObject>("Prefabs/Wall/WallH");
        m_WallCornerPrefabs = Resources.Load<GameObject>("Prefabs/Wall/Corner");
    }

    void Start() {
        List<GameObject> childGOList = new List<GameObject>();

        // Room parts
        m_RoomPartsContainer = this.transform.FindChild("RoomParts");
        int rpCount = m_RoomPartsContainer.childCount;
        for (int i = 0; i < rpCount; ++i) {
            RoomPartScript childRPScript = m_RoomPartsContainer.GetChild(i).GetComponent<RoomPartScript>();

            childGOList.Add(childRPScript.gameObject);
        }
        m_RoomParts = childGOList
            .Select(x => x.GetComponent<RoomPartScript>())
            .Where(y => y.m_IsReachable)
            .ToArray();

        // Walls
        m_WallsContainer = this.transform.FindChild("Walls");
        childGOList = childGOList.Where(x => !x.GetComponent<RoomPartScript>().m_IsReachable).ToList();
        int wCount = m_WallsContainer.childCount;
        for (int i = 0; i < wCount; ++i) {
            SceneryScript childSceneryScript = m_WallsContainer.GetChild(i).GetComponent<SceneryScript>();

            childGOList.Add(childSceneryScript.gameObject);
        }
        m_RoomWalls = childGOList.ToArray();

        this.GenerateRoomParts();
        this.GenerateWalls();
    }

    public void GenerateRoomParts() {
        int roomPartsCount = m_RoomParts.Length;
        for (int i = 0; i < roomPartsCount; ++i) {
            int rngRPPrefabIndex = Random.Range(0, c_RoomPartPrefabCount);
            if (m_RoomParts[i].m_RPIndex > -1
                && m_RoomParts[i].m_RPIndex < c_RoomPartPrefabCount) {
                rngRPPrefabIndex = m_RoomParts[i].m_RPIndex;
            }

            m_RoomParts[i].AttachContent(m_RoomPartPrefabs[rngRPPrefabIndex]);

            m_RoomParts[i].m_ParentRoom = this;
        }
    }

    void GenerateWalls() {
        int roomWallsCount = m_RoomWalls.Length;
        for (int i = 0; i < roomWallsCount; ++i) {
            SceneryScript.SCENERY_TYPE wallTypeToBuild = SceneryScript.SCENERY_TYPE.NONE;

            RoomPartScript currentRPScript = m_RoomWalls[i].GetComponent<RoomPartScript>();
            SceneryScript currentSScript = m_RoomWalls[i].GetComponent<SceneryScript>();
            if (currentRPScript == null) {
                wallTypeToBuild = currentSScript.m_Type;
            }
            else {
                wallTypeToBuild = currentRPScript.m_WallType;
            }

            Vector3 wallPosition = m_RoomWalls[i].transform.position;
            GameObject newWallGO = this.BuildWall(wallPosition, wallTypeToBuild);

            if (newWallGO == null) {
                continue;
            }
            else if (currentRPScript != null) {
                currentRPScript.AttachContent(newWallGO, true);
                currentRPScript.m_ParentRoom = this;
            }
            else {
                currentSScript.AttachContent(newWallGO);
            }
        }
    }

    GameObject BuildWall(Vector3 newWallPosition, SceneryScript.SCENERY_TYPE type) {
        int maxRNGRange = 0;
        GameObject prefab = null;
        bool isDoor = false;
        switch (type) {
            case SceneryScript.SCENERY_TYPE.DOOR_HOR:
            case SceneryScript.SCENERY_TYPE.DOOR_VER:
                isDoor = true;
                break;

            case SceneryScript.SCENERY_TYPE.CORNER:
                prefab = m_WallCornerPrefabs;
                break;
            case SceneryScript.SCENERY_TYPE.HORIZONTAL:
                prefab = m_WallHorPrefabs;
                break;
            case SceneryScript.SCENERY_TYPE.VERTICAL:
                prefab = m_WallVerPrefabs;
                break;

            case SceneryScript.SCENERY_TYPE.NONE:
                return new GameObject();
            default:
                Debug.LogError("RoomScript::BuildWall=> Wrong wall type!");
                break;
        }

        int rngIndex = Random.Range(0, maxRNGRange);
        GameObject newWall;
        if (isDoor) {
            newWall = null;
        }
        else {
            newWall = Object.Instantiate(prefab,
                newWallPosition, Quaternion.identity) as GameObject;
        }

        return newWall;
    }
}
