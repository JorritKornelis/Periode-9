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
    public GameObject sellTableUI;

    public Text nameText;
    public Text amountTextDisplay;
    public Sprite emptySlotSprite;

    [Header("Invetory")]
    public ItemClassScriptableObject itemScriptableObject;
    public SlotInformation[] slotInformationArray;

    public Color highLightColor;
    public Color colorReset;
    CharacterMovement charMovement;

    public StorageSystem storageSystemHolder;
    public SlotRefrenceInformation refrenceInformation1 = new SlotRefrenceInformation();
    public SlotRefrenceInformation refrenceInformation2 = new SlotRefrenceInformation();
    public SlotRefrenceInformation lastPressed = new SlotRefrenceInformation();

    [Header("Saving")]
    public Saving savingScript;
    public string savingTag;

    void Start()
    {
        inv.SetActive(false);
        chestPanel.SetActive(false);
        charMovement = gameObject.GetComponent<CharacterMovement>();

        savingScript = GameObject.FindWithTag(savingTag).GetComponent<Saving>();
        InvetoryLoadData();
        InvetorySaveData();
    }

    void Update()
    {
        OpenInventory();
    }

    void OpenInventory()
    {
        if (Input.GetButtonDown("Inventory"))
            if (inv.activeInHierarchy)
            {
                inv.SetActive(false);
                charMovement.allowMovement = true;
                chestPanel.SetActive(false);
                refrenceInformation1 = new SlotRefrenceInformation();
                lastPressed = new SlotRefrenceInformation();
                storageSystemHolder = null;
            }
            else
            {
                inv.SetActive(true);
                charMovement.allowMovement = false;
                UpdateInvetoryUI(slotInformationArray);
                Collider[] chestsCol = Physics.OverlapSphere(transform.position, charMovement.pickUpRadis, charMovement.itemLayer);
                for (int i = 0; i < chestsCol.Length; i++)
                    if (chestsCol[i].GetComponent<StorageSystem>())
                    {
                        chestPanel.SetActive(true);
                        storageSystemHolder = chestsCol[0].GetComponent<StorageSystem>();
                        UpdateInvetoryUI(storageSystemHolder.chestSlotArray);
                        break;
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
            lastPressed = refrenceInformation1;
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
            lastPressed = new SlotRefrenceInformation();
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

    //add items
    public void AddItem(int addItemIndex, int addAmount)
    {
        bool searchForNewSlot = true;
        for (int i = 0; i < slotInformationArray.Length; i++)
        {
            if (addItemIndex == slotInformationArray[i].index)
            {
                if (slotInformationArray[i].amount <= itemScriptableObject.itemInformationList[addItemIndex].maxStack)
                {
                    if (slotInformationArray[i].amount + addAmount > itemScriptableObject.itemInformationList[addItemIndex].maxStack)
                    {
                        int difference = itemScriptableObject.itemInformationList[addItemIndex].maxStack - slotInformationArray[i].amount;
                        slotInformationArray[i].amount += difference;
                        addAmount -= difference;
                    }
                    else
                    {
                        searchForNewSlot = false;
                        slotInformationArray[i].amount += addAmount;
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

    //update ui
    public void UpdateInvetoryUI(SlotInformation[] overloadArray)
    {
        for (int i = 0; i < overloadArray.Length; i++)
        {
            if (overloadArray[i].index >= 0 && overloadArray[i].amount > 0)
            {
                overloadArray[i].slotImage.sprite = itemScriptableObject.itemInformationList[overloadArray[i].index].Sprite;
            }
            else
            {
                overloadArray[i].slotImage.sprite = emptySlotSprite;
                overloadArray[i].index = -1;
            }
        }
        InvetorySaveData(); // verplaatsen naar sceneSwitch
    }

    public IEnumerator CoolDownItemDrop(float coolDown,GameObject itemGameObject)
    {
        itemGameObject.GetComponent<ItemIndex>().mayAdd = false;
        yield return new WaitForSeconds(coolDown);
        itemGameObject.GetComponent<ItemIndex>().mayAdd = true;
    }

    //inv naar data
    public void InvetorySaveData()
    {
        savingScript.data.inventory.Clear();

        for (int i = 0; i < slotInformationArray.Length; i++)
        {
            if (slotInformationArray[i].index > -1)
            {
                ItemSaveSlot slot = new ItemSaveSlot();
                slot.itemIndex = slotInformationArray[i].index;
                slot.slot = i;
                slot.amount = slotInformationArray[i].amount;
                savingScript.data.inventory.Add(slot);
            }
        }
    }
    
    //data naar inv 
    public void InvetoryLoadData()
    {
        foreach (var slot in savingScript.data.inventory)
        {
            slotInformationArray[slot.slot].index = slot.itemIndex;
            slotInformationArray[slot.slot].amount = slot.amount;
        }
        UpdateInvetoryUI(slotInformationArray);
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