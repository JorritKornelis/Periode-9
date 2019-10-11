using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cauldron : ShopAcessScript
{
    public CombinationInfo[] combinations;
    public int item1, item2;
    public Image item1Image, item2Image, craftedImage;
    public int craftedItem;
    public GameObject accepted, rejected;
    public float particleLifetime;
    public CameraFocus focus;
    public int focusIndex;
    public ItemClassScriptableObject items;
    public GameObject ui;

    public void ItemPlaceSlotClick(bool slot1)
    {
        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        if (inv.refrenceInformation1.taken)
        {
            SlotInformation invSlot = inv.slotInformationArray[inv.refrenceInformation1.witchIndex];
            int cash = -1;
            if (slot1)
            {
                cash = item1;
                item1 = invSlot.index;
            }
            else
            {
                cash = item2;
                item2 = invSlot.index;
            }
            invSlot.index = cash;
            invSlot.amount = (cash >= 0) ? 1 : 0;
            inv.refrenceInformation1 = new SlotRefrenceInformation();
            inv.UpdateInvetoryUI(inv.slotInformationArray);
        }
        UpdateSlots();
    }

    public IEnumerator CheckForPlaceSpot(bool slot1)
    {
        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        while (!inv.lastPressed.taken)
            yield return null;

        SlotInformation slot = inv.slotInformationArray[inv.lastPressed.witchIndex];
        int itemCash = (slot1)? item1 : item2;
        Image imageCash = (slot1) ? item1Image : item2Image;
;

        slot.index = itemCash;
        slot.amount = 1;

        UpdateSlots();
        inv.lastPressed = new SlotRefrenceInformation();
        inv.UpdateInvetoryUI(inv.slotInformationArray);
    }

    public void UpdateSlots()
    {
        if (item1 >= 0)
            item1Image.sprite = items.itemInformationList[item1].Sprite;
        else
            item1Image.sprite = null;
        if (item2 >= 0)
            item2Image.sprite = items.itemInformationList[item2].Sprite;
        else
            item2Image.sprite = null;
        if (craftedItem >= 0)
            craftedImage.sprite = items.itemInformationList[craftedItem].Sprite;
    }

    public override void Interact()
    {
        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        if (active)
        {
            active = false;
            ui.SetActive(false);
            inv.inv.SetActive(false);
        }
        else
        {
            StartCoroutine(focus.MoveTowardsPoint(focusIndex));
            inv.inv.SetActive(true);
            ui.SetActive(true);
            active = true;
        }
    }

    public void CheckForCrafting()
    {
        if(item1 != -1 && item2 != -1 && craftedItem == -1)
        {
            bool crafted = false;
            foreach (CombinationInfo info in combinations)
                if (item1 == info.items.x && item2 == info.items.y || item2 == info.items.x && item1 == info.items.y)
                {
                    craftedItem = info.combination;
                    crafted = true;
                    break;
                }
            item1 = -1;
            item2 = -1;
            Destroy(Instantiate(crafted ? accepted : rejected, transform.position, Quaternion.identity), particleLifetime);
            UpdateSlots();
        }
    }
}

[System.Serializable]
public class CombinationInfo
{
    public Vector2Int items;
    public int combination;
}
