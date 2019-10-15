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
    public string message;
}
