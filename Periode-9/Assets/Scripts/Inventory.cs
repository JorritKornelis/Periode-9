using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Public stuff")]
    public GameObject invObj;
    public GameObject extraInfoObj;
    bool mayMoveItem = false;
    public Text nameText;

    [Header("invetory")]
    public ItemClassScriptableObject itemScriptableObject;
    public SlotInformation[] slotInformationArray;

    public Color highLightColor;
    public Color colorReset;
    int indexHolder = 99;


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
    }

    public void ItemMove(int i)
    {
        if (mayMoveItem == true)
        {
            slotInformationArray[i].slotImage.color = highLightColor;
            slotInformationArray[i].slotImage.sprite = slotInformationArray[indexHolder].slotImage.sprite;

            slotInformationArray[indexHolder].slotImage.color = colorReset;
            slotInformationArray[indexHolder].slotImage.sprite = null;
            indexHolder = 99;
            mayMoveItem = false;
        }
        else if(slotInformationArray[i].slotImage.sprite != null && mayMoveItem == false)
        {
            slotInformationArray[i].slotImage.color = highLightColor;
            indexHolder = i;

            extraInfoObj.SetActive(true);
            //aanpassen
            nameText.text = itemScriptableObject.itemInformationList[i].name;
        }
    }

    public void SwitchItemButton()
    {
        extraInfoObj.SetActive(false);
        mayMoveItem = true;
    }
}

[System.Serializable]
public class SlotInformation
{
    public Image slotImage;
    public int index;
    public int amount;
    
}