using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActions : MonoBehaviour
{
    public DialogueSystem system;

    public void ToggleFollow()
    {
        system.following = !system.following;
    }

    public void InvertGameObjectActivity(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }
}
