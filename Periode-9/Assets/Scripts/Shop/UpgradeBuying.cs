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

    public UpgradeSlot[] shopUpgrades, healthUpgrades, swordUpgrades;

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
                shopUpgrades[i].bought = true;
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

        for (int i = 0; i < healthUpgrades.Length; i++)
        {
            healthUpgrades[i].priceDisplay.text = healthUpgrades[i].price.ToString();
            if(i == 0 || saving.data.unlocks.isUpgradeHealth)
            {
                healthUpgrades[i].bought = true;
                healthUpgrades[i].priceDisplay.gameObject.SetActive(false);
                if(i == 0 && !saving.data.unlocks.isUpgradeHealth || i == 1 && saving.data.unlocks.isUpgradeHealth)
                    healthUpgrades[i].selectedObject.SetActive(true);
                else
                    healthUpgrades[i].selectedObject.SetActive(false);
            }
            else
            {
                healthUpgrades[i].priceDisplay.gameObject.SetActive(true);
                healthUpgrades[i].selectedObject.SetActive(false);
            }
        }

        for (int i = 0; i < swordUpgrades.Length; i++)
        {
            swordUpgrades[i].priceDisplay.text = swordUpgrades[i].price.ToString();
            if (i == 0 || saving.data.unlocks.isUpgradeSword)
            {
                swordUpgrades[i].bought = true;
                swordUpgrades[i].priceDisplay.gameObject.SetActive(false);
                if (i == 0 && !saving.data.unlocks.isUpgradeSword || i == 1 && saving.data.unlocks.isUpgradeSword)
                    swordUpgrades[i].selectedObject.SetActive(true);
                else
                    swordUpgrades[i].selectedObject.SetActive(false);
            }
            else
            {
                swordUpgrades[i].priceDisplay.gameObject.SetActive(true);
                swordUpgrades[i].selectedObject.SetActive(false);
            }
        }
    }

    public void CheckToBuyShopUpgrade(int index)
    {
        if (!shopUpgrades[index].bought && saving.data.currency >= shopUpgrades[index].price)
        {
            saving.data.currency -= shopUpgrades[index].price;
            shopUpgrades[index].bought = true;
            saving.data.unlocks.isUpgradeShop = index;
            DisplayUpgrades();
        }
    }

    public void CheckToBuyHealthUpgrade(int index)
    {
        if (!healthUpgrades[index].bought && saving.data.currency >= healthUpgrades[index].price)
        {
            saving.data.currency -= healthUpgrades[index].price;
            healthUpgrades[index].bought = true;
            saving.data.unlocks.isUpgradeHealth = true;
            DisplayUpgrades();
        }
    }

    public void CheckToBuySwordUpgrade(int index)
    {
        if (!swordUpgrades[index].bought && saving.data.currency >= swordUpgrades[index].price)
        {
            saving.data.currency -= swordUpgrades[index].price;
            swordUpgrades[index].bought = true;
            saving.data.unlocks.isUpgradeSword = true;
            DisplayUpgrades();
        }
    }
}

[System.Serializable]
public class UpgradeSlot
{
    public int price;
    public GameObject selectedObject;
    public Text priceDisplay;
    public bool bought;
}
