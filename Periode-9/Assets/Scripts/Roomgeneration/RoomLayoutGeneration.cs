using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGeneration : MonoBehaviour
{
    public List<Vector2Int> roomLocations;
    public List<RoomInfo> roomInfos = new List<RoomInfo>();
    public int rooms;
    public Vector2Int roomRange;
    [Range(100,200)]
    public float sameDirectionValue = 125f;
    public DoorLocations doorLocations;
    public Vector2Int currentlyLocated;
    public int FloorSize;
    public GameObject floorObject;
    List<GameObject> currentFloors = new List<GameObject>();
    List<GameObject> currentDecoration = new List<GameObject>();
    public RoomListScriptableObject roomLayoutScriptableObject;
    public LayerMask groundMask;
    public Transform mapInfoCash;

    public void Start()
    {
        GenerateRooms(rooms, roomRange);
        DisplayRoom();
    }

    public void DisplayRoom()
    {
        DisplayDoors();
        GenerateFloor();
        SpawnDecoration();
    }

    public void DisplayItems()
    {

    }

    public void SpawnDecoration()
    {
        foreach (GameObject decoration in currentDecoration)
            if (decoration)
                Destroy(decoration);
        currentDecoration = new List<GameObject>();

        for (int i = 0; i < roomLocations.Count; i++)
            if (roomLocations[i] == currentlyLocated)
            {
                for (int number = 0; number < roomInfos[i].details.Count; number++)
                {
                    RoomDetailInfo detail = roomInfos[i].details[number];
                    GameObject temp = Instantiate(detail.obj, new Vector3(detail.location.x, 0, detail.location.y), Quaternion.identity, mapInfoCash);
                    temp.transform.Rotate(new Vector3(0, detail.randomRotation ? Random.Range(-180, 180) : detail.yRotation));
                    currentDecoration.Add(temp);
                }
                break;
            }
    }

    public void GenerateFloor()
    {
        foreach (GameObject floor in currentFloors)
            if(floor)
                Destroy(floor);
        currentFloors = new List<GameObject>();

        List<HoleInfo> holes = new List<HoleInfo>();
        for (int i = 0; i < roomLocations.Count; i++)
            if (roomLocations[i] == currentlyLocated)
            {
                holes = new List<HoleInfo>(roomInfos[i].holes);
                break;
            }

        for (int x = -FloorSize; x <= FloorSize; x++)
            for (int y = -FloorSize; y <= FloorSize; y++)
                currentFloors.Add(Instantiate(floorObject, new Vector3(x, -0.2f, y), Quaternion.identity,mapInfoCash));

        foreach(HoleInfo hole in holes)
        {
            Collider[] colliders = Physics.OverlapBox(new Vector3(hole.location.x, -0.2f, hole.location.y), new Vector3(hole.size.x, 1, hole.size.y), Quaternion.identity, groundMask);
            foreach (Collider col in colliders)
                Destroy(col.gameObject);
        }

    }

    public void DisplayDoors()
    {
        doorLocations.up.SetActive(roomLocations.Contains(currentlyLocated + new Vector2Int(0, 1)));
        doorLocations.down.SetActive(roomLocations.Contains(currentlyLocated + new Vector2Int(0, -1)));
        doorLocations.right.SetActive(roomLocations.Contains(currentlyLocated + new Vector2Int(1, 0)));
        doorLocations.left.SetActive(roomLocations.Contains(currentlyLocated + new Vector2Int(-1, 0)));
    }

    public void GenerateRooms(int roomAmount, Vector2Int pathChangeRange)
    {
        Vector2Int currentRoom = new Vector2Int(0, 0);
        roomInfos = new List<RoomInfo>();
        roomLocations.Add(currentRoom);
        roomInfos.Add(roomLayoutScriptableObject.startRoom);
        int currentSpree = 0;
        while (roomLocations.Count != roomAmount)
        {
            if (currentSpree >= Random.Range(pathChangeRange.x, pathChangeRange.y))
                currentRoom = roomLocations[Random.Range(0, roomLocations.Count)];
            Vector2Int sameDirection = new Vector2Int(1,0);
            int tries = 0;
            while (tries != GetSurroundingInfo(currentRoom))
            {
                float dir = Random.Range(0, sameDirectionValue);
                Vector2Int addValue = ((dir < 25) ? new Vector2Int(1, 0) : (dir < 50) ? new Vector2Int(-1, 0) : (dir < 75) ? new Vector2Int(0, 1) : (dir < 100)?new Vector2Int(0, -1) : sameDirection);
                if (!roomLocations.Contains(currentRoom + addValue))
                {
                    currentRoom += addValue;
                    sameDirection = addValue;
                    roomLocations.Add(currentRoom);
                    roomInfos.Add(roomLayoutScriptableObject.rooms[Random.Range(0, roomLayoutScriptableObject.rooms.Length)]);
                    break;
                }
            }
            currentSpree++;
        }
    }

    public int GetSurroundingInfo(Vector2Int checkLocation)
    {
        int available = 0;
        if (!roomLocations.Contains(checkLocation + new Vector2Int(1, 0)))
            available++;
        if (!roomLocations.Contains(checkLocation + new Vector2Int(-1, 0)))
            available++;
        if (!roomLocations.Contains(checkLocation + new Vector2Int(0, 1)))
            available++;
        if (!roomLocations.Contains(checkLocation + new Vector2Int(0, -1)))
            available++;

        return available;
    }

    public void OnDrawGizmos()
    {
        foreach (Vector2Int room in roomLocations)
        {
            Gizmos.color = (room == currentlyLocated) ? Color.red : Color.gray;
            Gizmos.DrawCube(new Vector3(room.x,3,room.y), Vector3.one);
        }
    }
}

[System.Serializable]
public class DoorLocations
{
    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;
}

[System.Serializable]
public class RoomInfo
{
    public HoleInfo[] holes;
    public List<RoomDetailInfo> details = new List<RoomDetailInfo>();
    public List<RoomEnemies> enemyLocations = new List<RoomEnemies>();
    public List<GameObject> items = new List<GameObject>();
}

[System.Serializable]
public class HoleInfo
{
    public Vector2Int location;
    public Vector2Int size;
}

[System.Serializable]
public class RoomDetailInfo
{
    public Vector2Int location;
    public GameObject obj;
    public float yRotation;
    public bool randomRotation;
}

[System.Serializable]
public class RoomEnemies
{
    Vector2Int location;
    public GameObject[] possibleEnemies;
}
