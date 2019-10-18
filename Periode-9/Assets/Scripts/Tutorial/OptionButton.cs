using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public DialogueSystem system;
    public DialogueInfo dialogue;
    public GameObject deactivationObject;

    public void OnButtonPressed()
    {
        deactivationObject.SetActive(false);
        system.StartCoroutine(system.StartDialogue(dialogue));
    }
}
