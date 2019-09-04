using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomInfo", menuName = "Rooms/RoomInfo")]
public class RoomListScriptableObject : ScriptableObject
{
    public RoomInfo startRoom;
    public RoomInfo[] rooms;
}
