using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Public stuff")]
    public GameObject inv;
    public GameObject extraInfoObj;
    public GameObject invPanel;
    public GameObject chestPanel;
    bool mayMoveItem = false;
    //bool mayDropItem = false;
    public Text nameText;
    public Text amountTextDisplay;

    [Header("invetory")]
    public ItemClassScriptableObject itemScriptableObject;
    public SlotInformation[] slotInformationArray;

    public Color highLightColor;
    public Color colorReset;
    int indexHolder = 99;
    CharacterMovement charMovement;

    public StorageSystem storageSystemHolder;
    SlotRefrenceInformation refrenceInformation1 = new SlotRefrenceInformation();
    SlotRefrenceInformation refrenceInformation2 = new SlotRefrenceInformation();

    void Start()
    {
        extraInfoObj.SetActive(false);
        inv.SetActive(false);
        chestPanel.SetActive(false);
        charMovement = gameObject.GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && inv.activeInHierarchy == false && chestPanel.activeInHierarchy == false && charMovement.allowMovement == true)
        {
            inv.SetActive(true);
            charMovement.allowMovement = false;
        }       
        else if(Input.GetButtonDown("Inventory") && inv.activeInHierarchy == true && chestPanel.activeInHierarchy == false && charMovement.allowMovement == false)
        {
            inv.SetActive(false);
            charMovement.allowMovement = true;
        }
    }

    public void SelectItem(int selectIndex, bool inTheChest)
    {
        if(refrenceInformation1.witchIndex == -1)
        {
            refrenceInformation1.witchIndex = selectIndex;
            refrenceInformation1.storageSystem = (inTheChest) ? storageSystemHolder : null;
        }
        else
        {
            refrenceInformation2.witchIndex = selectIndex;
            refrenceInformation2.storageSystem = (inTheChest) ? storageSystemHolder : null;
        }
        
        if(refrenceInformation1.witchIndex != -1 && refrenceInformation2.witchIndex != -1)
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

    public void DropItem(int indexDrop, SlotInformation slotInformation)
    {
        //slotInformationArray[indexDrop].itemGameobjectHolder.GetComponent<ItemIndex>().mayAdd = false;
        slotInformationArray[indexDrop].slotImage.color = colorReset;
        slotInformationArray[indexDrop].slotImage.sprite = null;
        //Insatniate object
        //GameObject g = Instantiate(slotInformationArray[indexDrop].itemGameobjectHolder, transform.position + (transform.forward*2), Quaternion.identity);
        //StartCoroutine(CoolDownItemDrop(2, g));
        //slotInformationArray[indexDrop].itemGameobjectHolder = null;
        //mayDropItem = false;
        indexDrop = 99;
    }

    public void DropItemButton()
    {
        extraInfoObj.SetActive(false);
        //mayDropItem = true;
        //DropItem(indexHolder,);
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
}