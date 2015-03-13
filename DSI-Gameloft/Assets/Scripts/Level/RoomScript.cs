using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RoomScript : MonoBehaviour {
    #region Members
    public GameObject[] m_RoomParts;
    public GameObject[] m_RoomPartPrefabs;

    // Walls
    public List<GameObject> m_RoomWalls;
    public GameObject[] m_WallHorPrefabs;
    public GameObject[] m_WallVerPrefabs;
    public GameObject[] m_WallDoorPrefabs;
    public GameObject[] m_WallCornerPrefabs;

    // Constants
    public const int c_MaxRoomPart = 9;
    public const int c_RoomPartPrefabCount = 2;
    public const int c_WallHorPrefabCount = 1;
    public const int c_WallVerPrefabCount = 1;
    public const int c_WallDoorPrefabCount = 1;
    public const int c_WallCornerPrefabCount = 1;
    #endregion

    void Awake () {
        m_RoomPartPrefabs = new GameObject[c_RoomPartPrefabCount];
        for (int i = 0; i < c_RoomPartPrefabCount; ++i) {
            m_RoomPartPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/RoomParts/RoomPart{0}", i));
        }

        m_WallHorPrefabs = new GameObject[c_WallHorPrefabCount];
        m_WallVerPrefabs = new GameObject[c_WallVerPrefabCount];
        m_WallDoorPrefabs = new GameObject[c_WallDoorPrefabCount];
        m_WallCornerPrefabs = new GameObject[c_WallCornerPrefabCount];

        int maxIterations = Mathf.Max (c_WallHorPrefabCount, c_WallVerPrefabCount,
            c_WallDoorPrefabCount, c_WallCornerPrefabCount);
        for (int i = 0; i < maxIterations; ++i) {
            m_WallHorPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryH{0}", i));
            m_WallVerPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryV{0}", i));
            m_WallDoorPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryD{0}", i));
            m_WallCornerPrefabs[i] = Resources.Load<GameObject> (string.Format ("Prefabs/Sceneries/SceneryC{0}", i));
        }
    }

    void Start () {
        if (m_RoomParts != null
            && m_RoomParts.Length != c_MaxRoomPart) {
            Debug.Log ("Room badly instanciated");

            m_RoomParts = new GameObject[c_MaxRoomPart];
            this.GenerateRoom ();
        }
        else {
            for (int i = 0; i < c_MaxRoomPart; ++i) {
                if (m_RoomParts[i] == null) {
                    m_RoomParts = new GameObject[c_MaxRoomPart];
                    this.GenerateRoom ();
                }
            }
        }
    }

    public void GenerateRoom () {
        Vector3 roomPartPosition = this.transform.position;
        roomPartPosition.x -= RoomPartScript.c_PartWidth;
        roomPartPosition.z += RoomPartScript.c_PartHeight;

        int roomPartsCount = m_RoomParts.Length;
        for (int i = 0; i < roomPartsCount; ++i) {
            int rngRPPrefabIndex = Random.Range (0, c_RoomPartPrefabCount);
            m_RoomParts[i] = Object.Instantiate (m_RoomPartPrefabs[rngRPPrefabIndex], roomPartPosition, Quaternion.identity) as GameObject;
            m_RoomParts[i].transform.parent = this.transform;
            m_RoomParts[i].name = string.Format ("RoomPart{0}", i.ToString ());
            RoomPartScript roomPartScript = m_RoomParts[i].GetComponent<RoomPartScript> ();
            roomPartScript.m_ParentRoom = this;
            roomPartScript.m_IsReachable = true;

            #region RoomPart position handling
            roomPartPosition.x += RoomPartScript.c_PartWidth;
            if (i % 3 == 2) {
                roomPartPosition = this.transform.position;
                roomPartPosition.x -= RoomPartScript.c_PartWidth;

                if (i == 5) {
                    roomPartPosition.z -= RoomPartScript.c_PartHeight;
                }
            }
            #endregion
        }

        this.GenerateWalls ();
    }

    void GenerateWalls () {
        m_RoomWalls = new List<GameObject> ();
        List<Transform> reachableRPTransforms = m_RoomParts
            .Select (x => x.transform)
            .Where (y => y.GetComponent<RoomPartScript> ().m_IsReachable).ToList ();
        int reachableRPCount = reachableRPTransforms.Count ();

        for (int i = 0; i < reachableRPCount; ++i) {
            Transform currentRPTransform = reachableRPTransforms[i];

            bool hasUpNeighbour = this.HasVerticalNeighbourPart (true, currentRPTransform, reachableRPTransforms);
            bool hasDownNeighbour = this.HasVerticalNeighbourPart (false, currentRPTransform, reachableRPTransforms);
            bool hasLeftNeighbour = this.HasHorizontalNeighbourPart (false, currentRPTransform, reachableRPTransforms);
            bool hasRightNeighbour = this.HasHorizontalNeighbourPart (true, currentRPTransform, reachableRPTransforms);

            //Debug.Log (string.Format ("{0}: {1} {2} {3} {4}",
            //    currentRPTransform.name,
            //    hasUpNeighbour ? "T" : "F",
            //    hasDownNeighbour ? "T" : "F",
            //    hasLeftNeighbour ? "T" : "F",
            //    hasRightNeighbour ? "T" : "F"));

            #region Corners
            Vector3 cornerPosition = currentRPTransform.position;
            if (!hasLeftNeighbour
                && !hasUpNeighbour) {
                cornerPosition.x += -RoomPartScript.c_PartWidth;
                cornerPosition.z += RoomPartScript.c_PartHeight;
            }
            else if (!hasLeftNeighbour
                && !hasDownNeighbour) {
                cornerPosition.x += -RoomPartScript.c_PartWidth;
                cornerPosition.z += -RoomPartScript.c_PartHeight;
            }
            else if (!hasRightNeighbour
                && !hasUpNeighbour) {
                cornerPosition.x += RoomPartScript.c_PartWidth;
                cornerPosition.z += RoomPartScript.c_PartHeight;
            }
            else if (!hasRightNeighbour
                && !hasDownNeighbour) {
                cornerPosition.x += RoomPartScript.c_PartWidth;
                cornerPosition.z += -RoomPartScript.c_PartHeight;
            }

            // Is there a corner to build
            if (cornerPosition != currentRPTransform.position) {
                Debug.Log ("Corner");
                int rngIndex = Random.Range (0, m_WallCornerPrefabs.Length);
                GameObject corner = Object.Instantiate (m_WallCornerPrefabs[rngIndex],
                    cornerPosition, Quaternion.identity) as GameObject;
                corner.transform.parent = this.transform;

                m_RoomWalls.Add (corner);
            }
            #endregion

            #region Walls
            #endregion

            #region Doors
            #endregion
        }

    }

    bool HasVerticalNeighbourPart (bool lookUp, Transform currentRPTransform, List<Transform> reachableRPTs) {
        int coef = lookUp
            ? 1 : -1;
        Vector3 positionToLook = currentRPTransform.position;
        positionToLook.z += coef * RoomPartScript.c_PartHeight;

        return reachableRPTs
            .Select (x => x.position)
            .Contains (positionToLook);
    }

    bool HasHorizontalNeighbourPart (bool lookRight, Transform currentRPTransform, List<Transform> reachableRPTs) {
        int coef = lookRight
            ? 1 : -1;
        Vector3 positionToLook = currentRPTransform.position;
        positionToLook.x += coef * RoomPartScript.c_PartWidth;

        return reachableRPTs
            .Select (x => x.position)
            .Contains (positionToLook);
    }
}
