using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomLayoutGeneration : MonoBehaviour
{
    #region Veriables
    private List<Vector2Int> roomLocations = new List<Vector2Int>();
    private List<RoomInfo> roomInfos = new List<RoomInfo>();
    private List<RoomInfoCash> roomCashes = new List<RoomInfoCash>();
    private List<GameObject> currentItems = new List<GameObject>();
    private List<GameObject> currentFloors = new List<GameObject>();
    private List<GameObject> currentDecoration = new List<GameObject>();
    [HideInInspector]
    public Vector2Int currentlyLocated;

    [Header("RoomGenInfo")]
    public int rooms;
    public Vector2Int roomRange;
    [Range(100,200)]
    public float sameDirectionValue = 125f;
    public int FloorSize;

    [Header("InfoAccess")]
    public RoomListScriptableObject roomLayoutScriptableObject;
    public ItemClassScriptableObject itemScriptableObject;

    [Header("Information")]
    public DoorLocations doorLocations;
    public GameObject floorObject;
    public LayerMask groundMask;
    public Transform mapInfoCash;
    public GameObject holeNav;
    public string enemyTag;
    public string itemTag;
    #endregion

    #region basic voids
    //Update
    ///Checks if enemies are dead
    public void Update()
    {
        if (!RoomClearInfo())
            if (GameObject.FindGameObjectsWithTag(enemyTag).Length == 0)
                RoomClearInfo(true);
    }

    //Start
    ///Generates the arena, and starts the display of the first room
    public void Start()
    {
        GenerateRooms(rooms, roomRange);
        DisplayRoom();
    }

    //DisplayRoom
    ///Calls all the voids to display a room
    public void DisplayRoom()
    {
        DisplayDoors();
        GenerateFloor();
        SpawnDecoration();
        if (!RoomClearInfo())
            SpawnEnemies();
        SpawnItems();
    }
    #endregion

    #region itemSpawn
    //SpawnItems
    ///Spawns the items
    public void SpawnItems()
    {
        currentItems = new List<GameObject>();
        ///locates the room info
        RoomInfoCash cash = new RoomInfoCash(true, new List<int>(), new List<Vector2Int>());
        for (int i = 0; i < roomLocations.Count; i++)
            if (roomLocations[i] == currentlyLocated)
            {
                cash = roomCashes[i];
                break;
            }
        ///Spawns the items
        foreach (ItemInformation item in cash.itemsInRoom)
            currentItems.Add(Instantiate(itemScriptableObject.itemInformationList[item.index].itemGameObject, new Vector3(item.location.x, 0, item.location.y), Quaternion.identity, mapInfoCash));
    }

    //UpdateItems
    ///Updates the items the are still there, and adds dropped items to the list
    public void UpdateItems()
    {
        ///locates the room
        for (int room = 0; room < roomLocations.Count; room++)
            if (roomLocations[room] == currentlyLocated)
            {
                ///checks if the items in the list are still there
                if(currentItems.Count != 0)
                    for (int item = 0; item < currentItems.Count; item++)
                        if (currentItems[item] == null)
                        {
                            roomCashes[room].itemsInRoom.RemoveAt(item);
                            currentItems.RemoveAt(item);
                            item--;
                        }
                        else
                            Destroy(currentItems[item]);
                ///Adds items that are not in the list (drops)
                GameObject[] otherItems = GameObject.FindGameObjectsWithTag(itemTag);
                if (otherItems.Length > 0)
                    foreach (GameObject item in otherItems)
                        roomCashes[room].itemsInRoom.Add(new ItemInformation(new Vector2Int(Mathf.RoundToInt(item.transform.position.x), Mathf.RoundToInt(item.transform.position.z)), item.GetComponent<ItemIndex>().index));
                break;
            }
    }
    #endregion

    #region enemy spawn && room cleared
    //SpawnEnemies
    ///Spawns the enemies in a room if it is not cleared
    public void SpawnEnemies()
    {
        for (int i = 0; i < roomLocations.Count; i++)
            if (roomLocations[i] == currentlyLocated)
            {
                for (int number = 0; number < roomInfos[i].enemyLocations.Count; number++)
                {
                    RoomEnemies enemy = roomInfos[i].enemyLocations[number];
                    currentDecoration.Add(Instantiate(enemy.Enemy, new Vector3(enemy.location.x, 0, enemy.location.y),Quaternion.identity));
                }
                break;
            }
    }

    //RoomClearInfo
    ///A simple function to check if a room is cleared
    ///This is called by the doors to check if it is allowed to move the player
    public bool RoomClearInfo()
    {
        for (int i = 0; i < roomLocations.Count; i++)
            if (roomLocations[i] == currentlyLocated)
                return roomCashes[i].cleared;

        return true;
    }

    //RoomClearInfo
    ///changes the info of a room to say its cleared
    public void RoomClearInfo(bool cleared)
    {
        for (int i = 0; i < roomLocations.Count; i++)
            if (roomLocations[i] == currentlyLocated)
                roomCashes[i].cleared = cleared;
    }
    #endregion

    #region roomVisuals
    //SpawnDecoration
    ///Spawns the decorations of a room
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
                    temp.transform.Rotate(new Vector3(0, detail.yRotation));
                    currentDecoration.Add(temp);
                }
                break;
            }
    }

    //GenerateFloor
    ///Generates the floor
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
            GameObject holNav = Instantiate(holeNav, new Vector3(hole.location.x, -0.2f, hole.location.y), Quaternion.identity);
            holNav.GetComponent<NavMeshObstacle>().size = new Vector3(hole.size.x * 2, 4, hole.size.y * 2);
            currentFloors.Add(holNav);
            foreach (Collider col in colliders)
                Destroy(col.gameObject);
        }
    }

    //DisplayDoor
    ///Checks what doors to display
    public void DisplayDoors()
    {
        doorLocations.up.SetActive(roomLocations.Contains(currentlyLocated + new Vector2Int(0, 1)));
        doorLocations.down.SetActive(roomLocations.Contains(currentlyLocated + new Vector2Int(0, -1)));
        doorLocations.right.SetActive(roomLocations.Contains(currentlyLocated + new Vector2Int(1, 0)));
        doorLocations.left.SetActive(roomLocations.Contains(currentlyLocated + new Vector2Int(-1, 0)));
    }
    #endregion

    #region roomGen
    //GenerateRooms
    ///Generates the roomLayout and assigns a random room info to it
    public void GenerateRooms(int roomAmount, Vector2Int pathChangeRange)
    {
        ///Adds the begin room
        Vector2Int currentRoom = new Vector2Int(0, 0);
        roomInfos = new List<RoomInfo>();
        roomLocations.Add(currentRoom);

        List<int> indexes = new List<int>();
        List<Vector2Int> locations = new List<Vector2Int>();
        for (int i = 0; i < roomLayoutScriptableObject.startRoom.items.Count; i++)
        {
            indexes.Add(roomLayoutScriptableObject.startRoom.items[i].index);
            locations.Add(roomLayoutScriptableObject.startRoom.items[i].location);
        }
        roomCashes.Add(new RoomInfoCash(false, indexes, locations));

        roomInfos.Add(roomLayoutScriptableObject.startRoom);
        int currentSpree = 0;
        ///Adds all the rooms
        while (roomLocations.Count != roomAmount)
        {
            ///Checks how long the spree is for room after room, then it selects a random room and continues from there
            if (currentSpree >= Random.Range(pathChangeRange.x, pathChangeRange.y))
                currentRoom = roomLocations[Random.Range(0, roomLocations.Count)];
            Vector2Int sameDirection = new Vector2Int(1,0);

            ///Tries to find a free spot around the current room
            int tries = 0;
            while (tries != GetSurroundingInfo(currentRoom))
            {
                float dir = Random.Range(0, sameDirectionValue);
                Vector2Int addValue = ((dir < 25) ? new Vector2Int(1, 0) : (dir < 50) ? new Vector2Int(-1, 0) : (dir < 75) ? new Vector2Int(0, 1) : (dir < 100)?new Vector2Int(0, -1) : sameDirection);
                if (!roomLocations.Contains(currentRoom + addValue))
                {
                    ///Adds a random room the the list
                    currentRoom += addValue;
                    sameDirection = addValue;
                    roomLocations.Add(currentRoom);

                    int index = Random.Range(0, roomLayoutScriptableObject.rooms.Length);
                    roomInfos.Add(roomLayoutScriptableObject.rooms[index]);
                    indexes = new List<int>();
                    locations = new List<Vector2Int>();
                    for (int i = 0; i < roomLayoutScriptableObject.rooms[index].items.Count; i++)
                    {
                        indexes.Add(roomLayoutScriptableObject.rooms[index].items[i].index);
                        locations.Add(roomLayoutScriptableObject.rooms[index].items[i].location);
                    }
                    roomCashes.Add(new RoomInfoCash(false, indexes,locations));
                    break;
                }
            }
            currentSpree++;
        }
    }

    //GetSurroundingInfo
    ///Checks how many doors are around the current room
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
    #endregion
}

#region constructors
//DoorTab
[System.Serializable]
public class DoorLocations
{
    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;
}
//RoomInfoCash
///Info that says if a room has been cleared, and has the item info
[System.Serializable]
public class RoomInfoCash
{
    public bool cleared;
    public List<ItemInformation> itemsInRoom = new List<ItemInformation>();

    public RoomInfoCash(bool _cleared, List<int> indexes,List<Vector2Int> locations)
    {
        cleared = _cleared;
        for (int i = 0; i < indexes.Count; i++)
            itemsInRoom.Add(new ItemInformation(locations[i], indexes[i]));
    }
}
//ItemInfo
[System.Serializable]
public class ItemInformation
{
    public Vector2Int location;
    public int index;

    public ItemInformation(Vector2Int _location, int _index)
    {
        location = _location;
        index = _index;
    }
}
#endregion
