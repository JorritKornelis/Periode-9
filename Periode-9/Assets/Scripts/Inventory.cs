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
    bool mayDropItem = false;
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
    SlotInformation slotInformationHolder;
    
    void Start()
    {
        /*foreach (Transform slot in invPanel.transform)
        {
            slotInformationList.Add(new SlotInformation() {slotImage = slot.GetComponent<Image>() });
        }
        slotInformationArray = slotInformationList.ToArray();
        */
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
    
    /*public void ItemMove(int i, SlotInformation[] slot)//lijst
    {
        if (mayMoveItem == true)
        {
            if (slot[i].index != slot[indexHolder].index)
            {
                Debug.Log("If INV");
                if (slot[i].slotImage.sprite == null)
                {
                    slot[i].slotImage.color = highLightColor;//lijst
                    slot[i].slotImage.sprite = slot[indexHolder].slotImage.sprite;
                    slot[i].itemGameobjectHolder = slot[indexHolder].itemGameobjectHolder;

                    slot[indexHolder].slotImage.color = colorReset;
                    slot[indexHolder].slotImage.sprite = null;
                    slot[indexHolder].itemGameobjectHolder = null;
                    indexHolder = 99;
                }
                else //aanpassen
                {
                    Sprite saveSprite = slot[i].slotImage.sprite;
                    GameObject tempGameObjecy = slot[i].itemGameobjectHolder;

                    slot[i].slotImage.color = highLightColor;
                    slot[i].itemGameobjectHolder = slot[indexHolder].itemGameobjectHolder;
                    slot[i].slotImage.sprite = slot[indexHolder].slotImage.sprite;

                    slot[indexHolder].slotImage.sprite = saveSprite;
                    slot[indexHolder].itemGameobjectHolder = tempGameObjecy;
                    slot[indexHolder].slotImage.color = colorReset;

                    indexHolder = 99;
                }

            }
            slot[i].slotImage.color = colorReset;
            mayMoveItem = false;
        }
        else if(slot[i].slotImage.sprite != null && mayMoveItem == false)
        {
            Debug.Log("ELSE IF INV");
            for (int it = 0; it < slot.Length; it++)
            {
                if (slot[it].slotImage.color == highLightColor)
                {
                    slot[it].slotImage.color = colorReset;
                    indexHolder = 99;
                    mayMoveItem = false;
                    continue;
                }
            }

            slot[i].slotImage.color = highLightColor;

            indexHolder = i;
            slotInformationHolder = slot[i];
            
            extraInfoObj.SetActive(true);
            //aanpassen
            //nameText.text = slotInformationArray[i].itemGameobjectHolder.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[slotInformationArray[i].itemGameobjectHolder.GetComponent<ItemIndex>().index].name;
            //amountTextDisplay.text = slotInformationArray[i].itemGameobjectHolder.GetComponent<ItemIndex>().amoundInItem.ToString();

        }
    }*/

    public void FirstSelect(int indexx, SlotInformation[] slot)
    {
        if (slot[indexx].index < 0)
        {

        }
    }


    public void DropItem(bool b, int indexDrop)
    {
        if (b == true)
        {
            //slotInformationArray[indexDrop].itemGameobjectHolder.GetComponent<ItemIndex>().mayAdd = false;
            slotInformationArray[indexDrop].slotImage.color = colorReset;
            slotInformationArray[indexDrop].slotImage.sprite = null;
            //Insatniate object
           //GameObject g = Instantiate(slotInformationArray[indexDrop].itemGameobjectHolder, transform.position + (transform.forward*2), Quaternion.identity);
            //StartCoroutine(CoolDownItemDrop(2, g));
            //slotInformationArray[indexDrop].itemGameobjectHolder = null;
            mayDropItem = false;
            indexDrop = 99;
        }
    }

    public void MoveItemButton()
    {
        extraInfoObj.SetActive(false);
        mayMoveItem = true;
    }

    public void DropItemButton()
    {
        extraInfoObj.SetActive(false);
        mayDropItem = true;
        DropItem(mayDropItem, indexHolder);
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
                    slotInformationArray[i].amount += addAmount;
                    addAmount = slotInformationArray[i].amount - itemScriptableObject.itemInformationList[addItemIndex].maxStack;
                    if (addAmount < 0)
                    {
                        searchForNewSlot = false;
                        break;
                    }
                    else
                    {
                        slotInformationArray[i].amount -= addAmount;
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
        UpdateInvetoryUI();//
    }

    void UpdateInvetoryUI()
    {
        for (int i = 0; i < slotInformationArray.Length; i++)
        {
            if (slotInformationArray[i].index >= 0)
            {
                Debug.Log(slotInformationArray[i].index);
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
    public int witchIndex;
    public StorageSystem storageSystem;
}