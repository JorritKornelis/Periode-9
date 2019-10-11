using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingUI : MonoBehaviour
{
    public GameObject ui;
    public SellPoint currentPoint;
    public Image image;
    public Inventory inv;
    public ItemClassScriptableObject itemList;
    public Sprite nothing;
    public Text[] priceInputs;
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
        DisplayPrice();
    }

    public void PanelClick()
    {
        if (currentCoroutine != null)
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
            inv.lastPressed = new SlotRefrenceInformation();
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
        
        SlotInformation slot = inv.slotInformationArray[inv.lastPressed.witchIndex];
        int itemCash = currentPoint.item;
        int amountCash = currentPoint.amount;

        currentPoint.item = slot.index;
        currentPoint.amount = slot.amount;
        if (currentPoint.item >= 0)
            image.sprite = itemList.itemInformationList[currentPoint.item].Sprite;
        else
            image.sprite = nothing;
        slot.index = itemCash;
        slot.amount = amountCash;

        currentPoint.DisplayItem();
        inv.lastPressed = new SlotRefrenceInformation();
        inv.UpdateInvetoryUI(inv.slotInformationArray);
        image.sprite = nothing;
    }

    public void PriceChange(int value)
    {
        if (currentPoint)
        {
            if(currentPoint.sellPrice + value >= 0 && currentPoint.sellPrice + value <= 999)
                currentPoint.sellPrice += value;
            DisplayPrice();
        }
    }

    public void DisplayPrice()
    {
        if (currentPoint)
        {
            float testValue = currentPoint.sellPrice;
            priceInputs[0].text = Mathf.Floor(testValue / 100).ToString();
            testValue -= Mathf.Floor(testValue / 100) * 100;
            priceInputs[1].text = Mathf.Floor(testValue / 10).ToString();
            testValue -= Mathf.Floor(testValue / 10) * 10;
            priceInputs[2].text = Mathf.Floor(testValue).ToString();
        }
    }
}
