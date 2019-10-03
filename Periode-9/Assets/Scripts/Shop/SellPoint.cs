using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPoint : MonoBehaviour
{
    public int item;
    public int amount;
    public int sellPrice;
    public Transform itemDisplay;
    public bool lookedAt;
    public bool displayItem;
    public int zoomIndex;
    public bool currentlyPlacable;
    public SellingUI ui;

    public void OnMouseDown()
    {
        if (currentlyPlacable)
            ui.SelectSellSlot(this);
    }

    public void DisplayItem()
    {
        foreach (Transform obj in itemDisplay.transform)
            Destroy(obj.gameObject);
        if (item >= 0)
            Instantiate(ui.itemList.itemInformationList[item].itemGameObject, itemDisplay.position, itemDisplay.rotation, itemDisplay);
    }
}
