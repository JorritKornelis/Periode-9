using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueInfo", menuName = "DialogueInfo")]
public class DialogueInfo : ScriptableObject
{
    public DialoguePartInfo[] dialogue;
}

[System.Serializable]
public class DialoguePartInfo
{
    public bool enthusiastic;
    [TextArea(3,7)]
    public string message;
    public int animationAmount = 2;
    public int soundAmount = 3;
    public int[] beginActionEvents, endActionEvents;
}
