using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopDoor : ShopAcessScript
{
    public GameObject shopUI;
    public BuyerSpawner spawner, spawner2;
    public Text openMessage;
    public Color openColor, closedColor;
    public string sceneSwitch;

    public void GoToDungeon()
    {
        SceneManager.LoadScene(sceneSwitch);
    }

    public void ShopOpenButton()
    {
        spawner.shopOpen = !spawner.shopOpen;
        spawner2.shopOpen = !spawner2.shopOpen;
        openMessage.color = spawner.shopOpen ? openColor : closedColor;
        openMessage.text = spawner.shopOpen ? "Open" : "Closed";
        foreach (GameObject buyer in GameObject.FindGameObjectsWithTag("Buyer"))
        {
            buyer.GetComponent<BuyerAI>().StopAllCoroutines();
            buyer.GetComponent<BuyerAI>().StartCoroutine(buyer.GetComponent<BuyerAI>().LeaveStore());
        }
    }

    public override void Interact()
    {
        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        if (active)
        {
            active = false;
            shopUI.SetActive(false);
            inv.inv.SetActive(false);
        }
        else
        {
            inv.inv.SetActive(false);
            shopUI.SetActive(true);
            active = true;
        }
    }
}
