﻿using System.Collections;
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

    [Header("invetory")]
    public ItemClassScriptableObject itemScriptableObject;
    public SlotInformation[] slotInformationArray;

    public Color highLightColor;
    public Color colorReset;
    int indexHolder = 99;
    //public List<SlotInformation> slotInformationList = new List<SlotInformation>();

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
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inv.SetActive(!inv.activeSelf);
        }
    }

    public void ItemMove(int i)
    {
        if (mayMoveItem == true)
        {
            if (slotInformationArray[i].index != slotInformationArray[indexHolder].index)
            {
                if (slotInformationArray[i].slotImage.sprite == null)
                {
                    slotInformationArray[i].slotImage.color = highLightColor;
                    slotInformationArray[i].slotImage.sprite = slotInformationArray[indexHolder].slotImage.sprite;
                    slotInformationArray[i].itemGameobjectHolder = slotInformationArray[indexHolder].itemGameobjectHolder;

                    slotInformationArray[indexHolder].slotImage.color = colorReset;
                    slotInformationArray[indexHolder].slotImage.sprite = null;
                    slotInformationArray[indexHolder].itemGameobjectHolder = null;
                    indexHolder = 99;
                }
                else
                {
                    Sprite saveSprite = slotInformationArray[i].slotImage.sprite;
                    GameObject tempGameObjecy = slotInformationArray[i].itemGameobjectHolder;

                    slotInformationArray[i].slotImage.color = highLightColor;
                    slotInformationArray[i].itemGameobjectHolder = slotInformationArray[indexHolder].itemGameobjectHolder;
                    slotInformationArray[i].slotImage.sprite = slotInformationArray[indexHolder].slotImage.sprite;

                    slotInformationArray[indexHolder].slotImage.sprite = saveSprite;
                    slotInformationArray[indexHolder].itemGameobjectHolder = tempGameObjecy;
                    slotInformationArray[indexHolder].slotImage.color = colorReset;

                    indexHolder = 99;
                }

            }
            slotInformationArray[i].slotImage.color = colorReset;
            mayMoveItem = false;
        }
        else if(slotInformationArray[i].slotImage.sprite != null && mayMoveItem == false)
        {
            for (int it = 0; it < slotInformationArray.Length; it++)
            {
                if (slotInformationArray[it].slotImage.color == highLightColor)
                {
                    slotInformationArray[it].slotImage.color = colorReset;
                    indexHolder = 99;
                    mayMoveItem = false;
                    continue;
                }
            }

            slotInformationArray[i].slotImage.color = highLightColor;

            indexHolder = i;
            
            extraInfoObj.SetActive(true);
            //aanpassen
            nameText.text = itemScriptableObject.itemInformationList[i].name;
        }
    }

    public void DropItem(bool b, int indexDrop)
    {
        if (b == true)
        {
            slotInformationArray[indexDrop].itemGameobjectHolder.GetComponent<ItemIndex>().mayAdd = false;
            slotInformationArray[indexDrop].slotImage.color = colorReset;
            slotInformationArray[indexDrop].slotImage.sprite = null;
            //Insatniate object
            GameObject g = Instantiate(slotInformationArray[indexDrop].itemGameobjectHolder, transform.position + (transform.forward*2), Quaternion.identity);
            StartCoroutine(CoolDownItemDrop(2, g));
            slotInformationArray[indexDrop].itemGameobjectHolder = null;
            mayDropItem = false;
            indexDrop = 99;
        }
    }

    public void SwitchItemButton()
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

    public void AddItem(int i, GameObject itemObject)
    {
        for (int forint = 0; forint < slotInformationArray.Length; forint++)
        {
            //add item
            if (slotInformationArray[forint].slotImage.sprite == itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].Sprite && itemObject.GetComponent<ItemIndex>().mayAdd == true && itemObject.GetComponent<ItemIndex>().amoundInItem < itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].maxStack)
            {
                Debug.Log("IN DE EERSTE IF");
                slotInformationArray[forint].slotImage.sprite = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].Sprite;
                slotInformationArray[forint].amount += itemObject.GetComponent<ItemIndex>().amoundInItem;
                slotInformationArray[forint].itemGameobjectHolder = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[itemObject.GetComponent<ItemIndex>().index].itemGameObject;
                Destroy(itemObject);
                break;
            }
            //next if amout is full
            else if (slotInformationArray[forint].slotImage.sprite == itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].Sprite && slotInformationArray[forint].amount + itemObject.GetComponent<ItemIndex>().amoundInItem > itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].maxStack)
            {
                Debug.Log("IN DE TWEEDE IF");
                int temp;
                temp = slotInformationArray[forint].amount + itemObject.GetComponent<ItemIndex>().amoundInItem - itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].maxStack;
                slotInformationArray[forint].amount = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].maxStack;
                if (i < slotInformationArray.Length)
                {
                    for (int iii = 0; iii < slotInformationArray.Length; iii++)
                    {
                        if (slotInformationArray[iii].slotImage.sprite == null)
                        {
                            slotInformationArray[i].slotImage.sprite = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].Sprite;
                            slotInformationArray[i].amount += temp;
                            slotInformationArray[i].itemGameobjectHolder = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[itemObject.GetComponent<ItemIndex>().index].itemGameObject;
                            Destroy(itemObject);
                            break;
                        }
                        else
                        {
                            itemObject.GetComponent<ItemIndex>().amoundInItem = temp;
                            Debug.Log("DE ELSE IN DE IFIF");
                            break;
                        }
                    }
                }
                break;
            }
            else if (slotInformationArray[forint].slotImage.sprite == null && itemObject.GetComponent<ItemIndex>().mayAdd == true && itemObject.GetComponent<ItemIndex>().amoundInItem < itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].maxStack)
            {
                Debug.Log("IN DE DEREDE IF");
                slotInformationArray[forint].slotImage.sprite = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[i].Sprite;
                slotInformationArray[forint].amount += itemObject.GetComponent<ItemIndex>().amoundInItem;
                slotInformationArray[forint].itemGameobjectHolder = itemObject.GetComponent<ItemIndex>().itemClassScriptableObject.itemInformationList[itemObject.GetComponent<ItemIndex>().index].itemGameObject;
                Destroy(itemObject);
                break;
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
    public int index;
    public int amount;
    public GameObject itemGameobjectHolder;
}