using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Public stuff")]
    public GameObject inv;
    public GameObject invPanel;
    public GameObject chestPanel;

    public Text nameText;
    public Text amountTextDisplay;

    [Header("invetory")]
    public ItemClassScriptableObject itemScriptableObject;
    public SlotInformation[] slotInformationArray;

    public Color highLightColor;
    public Color colorReset;
    CharacterMovement charMovement;

    public StorageSystem storageSystemHolder;
    public SlotRefrenceInformation refrenceInformation1 = new SlotRefrenceInformation();
    public SlotRefrenceInformation refrenceInformation2 = new SlotRefrenceInformation();

    void Start()
    {
        inv.SetActive(false);
        chestPanel.SetActive(false);
        charMovement = gameObject.GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (!charMovement.inTheChestBool)
        {
            if (Input.GetButtonDown("Inventory") && inv.activeInHierarchy == false && charMovement.allowMovement == true)
            {
                inv.SetActive(true);
                charMovement.allowMovement = false;
            }
            else if (Input.GetButtonDown("Inventory") && inv.activeInHierarchy == true && charMovement.allowMovement == false)
            {
                inv.SetActive(false);
                charMovement.allowMovement = true;
            }
        }
        else
        {
            if (Input.GetButtonDown("Inventory"))
            {
                if (chestPanel.activeSelf == false && inv.activeSelf == false)
                {
                    chestPanel.SetActive(true);
                    inv.SetActive(true);
                    charMovement.allowMovement = false;
                }
                else if (chestPanel.activeSelf == true && inv.activeSelf == true)
                {
                    chestPanel.SetActive(false);
                    inv.SetActive(false);
                    charMovement.allowMovement = true;
                    charMovement.inTheChestBool = false;
                    Debug.Log("TEST");
                }
            }

        }
    }
    public void SelectItem(int selectIndex, bool inTheChest)
    {
        if(!refrenceInformation1.taken)
        {
            refrenceInformation1.witchIndex = selectIndex;
            refrenceInformation1.storageSystem = (inTheChest) ? storageSystemHolder : null;
            refrenceInformation1.taken = true;
            if (((inTheChest) ? storageSystemHolder.chestSlotArray[selectIndex].index : slotInformationArray[selectIndex].index) == -1)
                refrenceInformation1 = new SlotRefrenceInformation();
        }
        else
        {
            refrenceInformation2.witchIndex = selectIndex;
            refrenceInformation2.storageSystem = (inTheChest) ? storageSystemHolder : null;
            refrenceInformation2.taken = true;
        }
        
        if(refrenceInformation1.taken && refrenceInformation2.taken)
        {
            ItemMove();
        }
    }
    
    public void ItemMove()
    {
        int cashIndex;
        int cashAmount;
        SlotInformation slot1 = new SlotInformation();
        SlotInformation slot2 = new SlotInformation();

        if (refrenceInformation1.storageSystem == null)
        {
            slot1 = slotInformationArray[refrenceInformation1.witchIndex];
        }
        else
        {
            slot1 = storageSystemHolder.chestSlotArray[refrenceInformation1.witchIndex];
        }
        if (refrenceInformation2.storageSystem == null)
        {
            slot2 = slotInformationArray[refrenceInformation2.witchIndex];
        }
        else
        {
            slot2 = storageSystemHolder.chestSlotArray[refrenceInformation2.witchIndex];
        }

        cashIndex = slot1.index;
        cashAmount = slot1.amount;

        slot1.index = slot2.index;
        slot1.amount = slot2.amount;
        slot2.index = cashIndex;
        slot2.amount = cashAmount;
        UpdateInvetoryUI(slotInformationArray);
        if (storageSystemHolder)
        {
            UpdateInvetoryUI(storageSystemHolder.chestSlotArray);
        }
        refrenceInformation1 = new SlotRefrenceInformation();
        refrenceInformation2 = new SlotRefrenceInformation();
    }

    public void DropItemButton()
    {
        if(refrenceInformation1.taken)
        {
            SlotInformation slot = new SlotInformation();

            if (refrenceInformation1.storageSystem == null)
                slot = slotInformationArray[refrenceInformation1.witchIndex];
            else
                slot = storageSystemHolder.chestSlotArray[refrenceInformation1.witchIndex];

            GameObject g = Instantiate(itemScriptableObject.itemInformationList[slot.index].itemGameObject, transform.position + (transform.forward * 2), Quaternion.identity);
            g.GetComponent<ItemIndex>().amoundInItem = slot.amount;
            StartCoroutine(CoolDownItemDrop(0.3f, g));
            slot.index = -1;
            slot.amount = 0;
            refrenceInformation1 = new SlotRefrenceInformation();

            UpdateInvetoryUI(slotInformationArray);
            if (storageSystemHolder)
            {
                UpdateInvetoryUI(storageSystemHolder.chestSlotArray);
            }
        }
    }

    public void AddItem(int addItemIndex, int addAmount)
    {
        bool searchForNewSlot = true;
        for (int i = 0; i < slotInformationArray.Length; i++)
        {
            if (addItemIndex == slotInformationArray[i].index)
            {
                if (slotInformationArray[i].amount <= itemScriptableObject.itemInformationList[addItemIndex].maxStack)
                {
                    if(slotInformationArray[i].amount + addAmount > itemScriptableObject.itemInformationList[addItemIndex].maxStack)
                    {
                        int difference = itemScriptableObject.itemInformationList[addItemIndex].maxStack - slotInformationArray[i].amount;
                        slotInformationArray[i].amount += difference;
                        addAmount -= difference;
                    }
                    else
                    {
                        searchForNewSlot = false;
                        break;
                    }
                }
            }
        }
        if (searchForNewSlot)
        {
            for (int i = 0; i < slotInformationArray.Length; i++)
            {
                if (slotInformationArray[i].index < 0)
                {
                    slotInformationArray[i].amount = addAmount;
                    slotInformationArray[i].index = addItemIndex;
                    break;
                }
            }
        }
        UpdateInvetoryUI(slotInformationArray);
    }

    void UpdateInvetoryUI(SlotInformation[] overloadArray)
    {
        for (int i = 0; i < overloadArray.Length; i++)
        {
            if (overloadArray[i].index >= 0)
            {
                Debug.Log(overloadArray[i].index + " UPDATE UI");
                overloadArray[i].slotImage.sprite = itemScriptableObject.itemInformationList[overloadArray[i].index].Sprite;
            }
            else
            {
                overloadArray[i].slotImage.sprite = null;
            }
        }
    }

    public IEnumerator CoolDownItemDrop(float coolDown,GameObject itemGameObject)
    {
        itemGameObject.GetComponent<ItemIndex>().mayAdd = false;
        yield return new WaitForSeconds(coolDown);
        itemGameObject.GetComponent<ItemIndex>().mayAdd = true;
    }
}

[System.Serializable]
public class SlotInformation
{
    public Image slotImage;
    public int index = -1;
    public int amount;
}

[System.Serializable]
public class SlotRefrenceInformation
{
    public int witchIndex = -1;
    public StorageSystem storageSystem;
    public bool taken;
}