using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGeneration : MonoBehaviour
{
    public List<Vector2Int> roomLocations;

    public void Start()
    {
        StartCoroutine(GenerateRooms(14, new Vector2Int(3, 6)));
    }

    public IEnumerator GenerateRooms(int roomAmount, Vector2Int pathChangeRange)
    {
        Vector2Int currentRoom = new Vector2Int(0, 0);
        roomLocations.Add(currentRoom);
        int currentSpree = 0;
        while (roomLocations.Count != roomAmount)
        {
            if (currentSpree >= Random.Range(pathChangeRange.x, pathChangeRange.y))
                currentRoom = roomLocations[Random.Range(0, roomLocations.Count)];

            int tries = 0;
            while (tries != GetSurroundingInfo(currentRoom))
            {
                yield return new WaitForSeconds(0.3f);
                float dir = Random.Range(0, 100);
                Vector2Int addValue = ((dir < 25) ? new Vector2Int(1, 0) : (dir < 50) ? new Vector2Int(-1, 0) : (dir < 75) ? new Vector2Int(0, 1) : new Vector2Int(0, -1));
                if (!roomLocations.Contains(currentRoom + addValue))
                {
                    currentRoom += addValue;
                    roomLocations.Add(currentRoom);
                    break;
                }
            }
            currentSpree++;
        }
        yield return new WaitForSeconds(0.3f);
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
            Gizmos.DrawCube(new Vector3(room.x,3,room.y), Vector3.one);
        }
    }
}

[System.Serializable]
public class RoomInfo
{
    public Vector2Int roomLocation;
    public RoomInfo(Vector2Int _RoomLocation)
    {
        roomLocation = _RoomLocation;
    }
}
