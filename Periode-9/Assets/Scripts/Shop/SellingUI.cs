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
    public IEnumerator currentCoroutine;

    public void SelectSellSlot(SellPoint point)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
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
        StopCoroutine(currentCoroutine);
        if (inv.refrenceInformation1.taken)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            SlotInformation slot = new SlotInformation();
            if (inv.refrenceInformation1.storageSystem == null)
                slot = inv.slotInformationArray[inv.refrenceInformation1.witchIndex];
            else
                slot = inv.storageSystemHolder.chestSlotArray[inv.refrenceInformation1.witchIndex];

            int itemCash = currentPoint.item;
            int amountCash = currentPoint.amount;
            currentPoint.item = slot.index;
            currentPoint.amount = slot.amount;
            image.sprite = itemList.itemInformationList[slot.index].Sprite;
            slot.index = itemCash;
            slot.amount = amountCash;

            currentPoint.DisplayItem();
            inv.refrenceInformation1 = new SlotRefrenceInformation();
            inv.UpdateInvetoryUI(inv.slotInformationArray);
        }
        else if(currentPoint.item >= 0)
        {
            currentCoroutine = CheckForPlaceSpot();
            StartCoroutine(currentCoroutine);
        }
    }

    public IEnumerator CheckForPlaceSpot()
    {
        while (!inv.lastPressed.taken)
            yield return null;

        SlotInformation slot = new SlotInformation();
        int itemCash = currentPoint.item;
        int amountCash = currentPoint.amount;
        currentPoint.item = slot.index;
        currentPoint.amount = slot.amount;
        image.sprite = itemList.itemInformationList[slot.index].Sprite;
        slot.index = itemCash;
        slot.amount = amountCash;

        currentPoint.DisplayItem();
        inv.lastPressed = new SlotRefrenceInformation();
        inv.UpdateInvetoryUI(inv.slotInformationArray);
        image.sprite = nothing;
    }
}
