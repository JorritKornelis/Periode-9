using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomInfo", menuName = "Rooms/RoomInfo")]
public class RoomListScriptableObject : ScriptableObject
{
    public RoomInfo startRoom;
    public RoomInfo[] rooms;
}

[System.Serializable]
public class RoomInfo
{
    public HoleInfo[] holes;
    public List<RoomDetailInfo> details = new List<RoomDetailInfo>();
    public List<RoomEnemies> enemyLocations = new List<RoomEnemies>();
    public List<ItemInformation> items = new List<ItemInformation>();
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
}

[System.Serializable]
public class RoomEnemies
{
    public Vector2Int location;
    public GameObject Enemy;
}
