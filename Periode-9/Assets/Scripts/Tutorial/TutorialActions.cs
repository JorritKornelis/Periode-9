using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActions : MonoBehaviour
{
    public DialogueSystem system;
    public string playerTag;

    public void ToggleFollow()
    {
        system.following = !system.following;
    }

    public void InvertGameObjectActivity(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }

    public void CharacterAllowMovement(bool allow)
    {
        Debug.Log(allow);
        GameObject.FindWithTag(playerTag).GetComponent<CharacterMovement>().allowMovement = allow;
    }

    public void DissableSkullFollowing()
    {
        StartCoroutine(system.SendSkullToPedastal());
    }

    public void TeleportPlayer(Transform telepoint)
    {
        GameObject.FindWithTag(playerTag).transform.position = telepoint.position;
    }

    public void DialogueDeactivate()
    {
        system.active = false;
    }

    public void HadShopTutorial()
    {
        system.saving.data.hadTutorialShop = true;
        system.saving.SaveData();
    }

    public void HadDungeonTutorial()
    {
        system.saving.data.hadTutorialDungeon = true;
        system.saving.SaveData();
    }
}
