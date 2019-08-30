using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject invObj;
    public GameObject extraInfoObj;

    public ItemClassScriptableObject itemScriptableObject;
    public SlotInformation[] slotInformationArray;

    public Color highLightColor;
    SlotInformation SlotInformationHolder;
    int indexHolder;

    void Start()
    {
        extraInfoObj.SetActive(false);
        invObj.SetActive(false);


    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            invObj.SetActive(!invObj.activeSelf);
        }
        TempPIckUPItem();
    }

    void TempPIckUPItem()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            foreach (SlotInformation slot in slotInformationArray)
            {
                slot.slotImage.sprite = itemScriptableObject.itemInformationList[slot.index].Sprite;
            }
        }
    }

    public void ItemMove(int i)
    {



    }









    /*
    public void ItemMove(int i)
    {
        if (SlotInformationHolder != null && i != indexHolder)
        {
            slotInformationArray[i] = SlotInformationHolder;
        }
        else if(SlotInformationHolder.slotImage != null)
        {
            slotInformationArray[i].slotImage.color = highLightColor;
            indexHolder = i;
            SlotInformationHolder = slotInformationArray[i];
            extraInfoObj.SetActive(true);
            //set info gelijk
        }
    }

    public void SwitchItemButton()
    {
        extraInfoObj.SetActive(false);

    }
    */
}

[System.Serializable]
public class SlotInformation
{
    public Image slotImage;
    public int index;
    public int amount;
}