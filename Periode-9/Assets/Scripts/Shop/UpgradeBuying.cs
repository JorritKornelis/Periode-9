using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuying : ShopAcessScript
{
    public GameObject upgradeUI;
    public DialogueSystem dialogue;
    public DialogueInfo[] dialogues;
    public Saving saving;
    public string savingTag;

    public UpgradeSlot[] shopUpgrades;

    public void Start()
    {
        saving = GameObject.FindWithTag(savingTag).GetComponent<Saving>();
        DisplayUpgrades();
    }

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

    public void DisplayUpgrades()
    {
        for (int i = 0; i < shopUpgrades.Length; i++)
        {
            shopUpgrades[i].priceDisplay.text = shopUpgrades[i].price.ToString();
            if (i <= saving.data.unlocks.isUpgradeShop)
            {
                shopUpgrades[i].priceDisplay.gameObject.SetActive(false);
                if (i == saving.data.unlocks.isUpgradeShop)
                    shopUpgrades[i].selectedObject.SetActive(true);
                else
                    shopUpgrades[i].selectedObject.SetActive(false);
            }
            else
            {
                shopUpgrades[i].priceDisplay.gameObject.SetActive(true);
                shopUpgrades[i].selectedObject.SetActive(false);
            }

        }
    }
}

[System.Serializable]
public class UpgradeSlot
{
    public int price;
    public GameObject selectedObject;
    public Text priceDisplay;
}
