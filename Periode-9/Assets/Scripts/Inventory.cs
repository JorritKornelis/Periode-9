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
    SlotRefrenceInformation refrenceInformation3 = new SlotRefrenceInformation();
    
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
        Debug.Log(refrenceInformation1.witchIndex + " TEST");
        Debug.Log(refrenceInformation2.witchIndex + " TEST");
        refrenceInformation3.witchIndex = refrenceInformation1.witchIndex;

        refrenceInformation1.witchIndex = refrenceInformation2.witchIndex;

        refrenceInformation2.witchIndex = refrenceInformation3.witchIndex;
        UpdateInvetoryUI();
        Debug.Log("TEST ITEM MOVE");
        Debug.Log(refrenceInformation1.witchIndex +" TEST ITEM MOVE");
        Debug.Log(refrenceInformation2.witchIndex +" TEST ITEM MOVE");
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
        UpdateInvetoryUI();
    }

    void UpdateInvetoryUI()
    {
        for (int i = 0; i < slotInformationArray.Length; i++)
        {
            if (slotInformationArray[i].index >= 0)
            {
                Debug.Log(slotInformationArray[i].index + " UPDATE UI");
                slotInformationArray[i].slotImage.sprite = itemScriptableObject.itemInformationList[slotInformationArray[i].index].Sprite;
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