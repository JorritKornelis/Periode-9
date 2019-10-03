using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingUI : MonoBehaviour
{
    public GameObject ui;
    public SellPoint currentPoint;
    public Text valueInput;
    public Image image;
    public Inventory inv;
    public ItemClassScriptableObject itemList;
    public Sprite nothing;

    public void SelectSellSlot(SellPoint point)
    {
        ui.SetActive(true);
        currentPoint = point;
        if (point.item >= 0)
            image.sprite = itemList.itemInformationList[point.item].Sprite;
        else
            image.sprite = nothing;
        valueInput.text = point.sellPrice.ToString();
    }

    public void PanelClick()
    {
        if (inv.refrenceInformation1.taken)
        {
            SlotInformation slot = new SlotInformation();
            if (inv.refrenceInformation1.storageSystem == null)
                slot = inv.slotInformationArray[inv.refrenceInformation1.witchIndex];
            else
                slot = inv.storageSystemHolder.chestSlotArray[inv.refrenceInformation1.witchIndex];

            currentPoint.item = slot.index;
            currentPoint.amount = slot.amount;
            currentPoint.DisplayItem();
            inv.refrenceInformation1 = new SlotRefrenceInformation();
        }
    }
}
