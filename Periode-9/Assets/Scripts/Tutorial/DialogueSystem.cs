using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public AnimationClip[] normalAnimations;
    public AnimationClip[] enthusiasticAnimations;
    public Text textInput;
    public GameObject uiPanel;

    public void StartDialogue(DialogueInfo info)
    {
        uiPanel.SetActive(true);
    }
}
