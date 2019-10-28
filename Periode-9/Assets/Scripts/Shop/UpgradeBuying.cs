using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuying : ShopAcessScript
{
    public GameObject upgradeUI;
    public DialogueSystem dialogue;
    public DialogueInfo[] dialogues;

    public override void Interact()
    {
        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        if (active)
        {
            active = false;
            upgradeUI.SetActive(false);
            inv.inv.SetActive(false);
        }
        else
        {
            inv.inv.SetActive(false);
            active = true;
            StartCoroutine(dialogue.StartDialogue(dialogues[Random.Range(0, dialogues.Length)]));
        }
    }
}
