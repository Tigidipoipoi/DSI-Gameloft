﻿using UnityEngine;
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
    GameObject[] m_RoomParts;
    GameObject[] m_RoomPartPrefabs;
    Transform m_RoomPartsContainer;

    // Walls
    GameObject[] m_RoomWalls;
    GameObject[] m_WallHorPrefabs;
    GameObject[] m_WallVerPrefabs;
    GameObject[] m_WallHorDoorPrefabs;
    GameObject[] m_WallVerDoorPrefabs;
    GameObject[] m_WallCornerPrefabs;
    Transform m_WallsContainer;

    // Constants
    public const int c_RoomPartPrefabCount = 2;
    public const int c_WallHorPrefabCount = 1;
    public const int c_WallVerPrefabCount = 1;
    public const int c_WallHorDoorPrefabCount = 1;
    public const int c_WallVerDoorPrefabCount = 1;
    public const int c_WallCornerPrefabCount = 1;
    #endregion

    void Awake () {
        m_RoomPartPrefabs = new GameObject[c_RoomPartPrefabCount];
        for (int i = 0; i < c_RoomPartPrefabCount; ++i) {
            m_RoomPartPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/RoomParts/RoomPart{0}", i));
        }

        m_WallHorPrefabs = new GameObject[c_WallHorPrefabCount];
        m_WallVerPrefabs = new GameObject[c_WallVerPrefabCount];
        m_WallHorDoorPrefabs = new GameObject[c_WallHorDoorPrefabCount];
        m_WallVerDoorPrefabs = new GameObject[c_WallVerDoorPrefabCount];
        m_WallCornerPrefabs = new GameObject[c_WallCornerPrefabCount];

        int maxIterations = Mathf.Max (c_WallHorPrefabCount, c_WallVerPrefabCount,
            c_WallHorDoorPrefabCount, c_WallVerDoorPrefabCount, c_WallCornerPrefabCount);
        for (int i = 0; i < maxIterations; ++i) {
            if (i < c_WallHorPrefabCount) {
                m_WallHorPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryH{0}", i));
            }
            if (i < c_WallVerPrefabCount) {
                m_WallVerPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryV{0}", i));
            }
            if (i < c_WallHorDoorPrefabCount) {
                m_WallHorDoorPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryDH{0}", i));
            }
            if (i < c_WallVerDoorPrefabCount) {
                m_WallVerDoorPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryDV{0}", i));
            }
            if (i < c_WallCornerPrefabCount) {
                m_WallCornerPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryC{0}", i));
            }
        }
    }

    void Start () {
        List<GameObject> childGOList = new List<GameObject> ();

        // Room parts
        m_RoomPartsContainer = this.transform.FindChild ("RoomParts");
        int rpCount = m_RoomPartsContainer.childCount;
        for (int i = 0; i < rpCount; ++i) {
            RoomPartScript childRPScript = m_RoomPartsContainer.GetChild (i).GetComponent<RoomPartScript> ();

            childGOList.Add (childRPScript.gameObject);
        }
        m_RoomParts = childGOList.Where (x => x.GetComponent<RoomPartScript> ().m_IsReachable).ToArray ();

        // Walls
        m_WallsContainer = this.transform.FindChild ("Walls");
        childGOList = childGOList.Where (x => !x.GetComponent<RoomPartScript> ().m_IsReachable).ToList ();
        int wCount = m_WallsContainer.childCount;
        for (int i = 0; i < wCount; ++i) {
            SceneryScript childSceneryScript = m_WallsContainer.GetChild (i).GetComponent<SceneryScript> ();

            childGOList.Add (childSceneryScript.gameObject);
        }
        m_RoomWalls = childGOList.ToArray ();

        this.GenerateRoom ();
        this.GenerateWalls ();
    }

    public void GenerateRoom () {
        int roomPartsCount = m_RoomParts.Length;
        for (int i = 0; i < roomPartsCount; ++i) {
            RoomPartScript roomPartScript = m_RoomParts[i].GetComponent<RoomPartScript> ();
            int rngRPPrefabIndex = Random.Range (0, c_RoomPartPrefabCount);

            roomPartScript.AttachContent (m_RoomPartPrefabs[rngRPPrefabIndex]);

            roomPartScript.m_ParentRoom = this;
        }
    }

    void GenerateWalls () {
        int roomWallsCount = m_RoomWalls.Length;
        for (int i = 0; i < roomWallsCount; ++i) {
            SceneryScript.SCENERY_TYPE wallTypeToBuild = SceneryScript.SCENERY_TYPE.NONE;

            RoomPartScript currentRPScript = m_RoomWalls[i].GetComponent<RoomPartScript> ();
            SceneryScript currentSScript = m_RoomWalls[i].GetComponent<SceneryScript> ();
            if (currentRPScript == null) {
                wallTypeToBuild = currentSScript.m_Type;
            }
            else {
                wallTypeToBuild = currentRPScript.m_WallType;
            }

            Vector3 wallPosition = m_RoomWalls[i].transform.position;
            GameObject newWallGO = this.BuildWall (wallPosition, wallTypeToBuild);

            if (currentRPScript != null) {
                currentRPScript.AttachContent (newWallGO, true);
                currentRPScript.m_ParentRoom = this;
            }
            else {
                currentSScript.AttachContent (newWallGO);
            }
        }
    }

    GameObject BuildWall (Vector3 newWallPosition, SceneryScript.SCENERY_TYPE type) {
        int maxRNGRange = 0;
        GameObject[] prefabs = null;
        switch (type) {
            case SceneryScript.SCENERY_TYPE.CORNER:
                maxRNGRange = m_WallCornerPrefabs.Length;
                prefabs = m_WallCornerPrefabs;
                break;
            case SceneryScript.SCENERY_TYPE.DOOR_HOR:
                maxRNGRange = m_WallHorDoorPrefabs.Length;
                prefabs = m_WallHorDoorPrefabs;
                break;
            case SceneryScript.SCENERY_TYPE.DOOR_VER:
                maxRNGRange = m_WallVerDoorPrefabs.Length;
                prefabs = m_WallVerDoorPrefabs;
                break;
            case SceneryScript.SCENERY_TYPE.HORIZONTAL:
                maxRNGRange = m_WallHorPrefabs.Length;
                prefabs = m_WallHorPrefabs;
                break;
            case SceneryScript.SCENERY_TYPE.VERTICAL:
                maxRNGRange = m_WallVerPrefabs.Length;
                prefabs = m_WallVerPrefabs;
                break;

            case SceneryScript.SCENERY_TYPE.NONE:
                return new GameObject ();
            default:
                Debug.LogError ("RoomScript::BuildWall=> Wrong wall type!");
                break;
        }

        int rngIndex = Random.Range (0, maxRNGRange);
        GameObject newWall = Object.Instantiate (prefabs[rngIndex],
            newWallPosition, Quaternion.identity) as GameObject;

        return newWall;
    }
}
