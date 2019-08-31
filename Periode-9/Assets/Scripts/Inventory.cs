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
    public Color colorReset;
    int indexHolder = 99;

    bool mayMoveItem = false;
    public Text extraInformationName;

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
        Debug.Log(indexHolder + " Index Holder");
    }

    void TempPIckUPItem()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            int temp = 0;
            slotInformationArray[temp].slotImage.sprite = itemScriptableObject.itemInformationList[temp].Sprite;
            temp++;
        }
    }

    public void ItemMove(int i)
    {
        Debug.Log("TETVDBBIABVUICVIYAVIYCAVYCVIYVCA");
        if (/*i != indexHolder && slotInformationArray[i].slotImage == null &&*/ mayMoveItem == true)
        {
            Debug.Log("if");
            slotInformationArray[i].slotImage.color = highLightColor;
            slotInformationArray[i].slotImage.sprite = slotInformationArray[indexHolder].slotImage.sprite;

            slotInformationArray[indexHolder].slotImage.color = colorReset;
            slotInformationArray[indexHolder].slotImage.sprite = null;
            indexHolder = 99;
            mayMoveItem = false;
        }
        else if(slotInformationArray[i].slotImage != null && mayMoveItem == false)
        {
            Debug.Log("Esle");
            slotInformationArray[i].slotImage.color = highLightColor;
            indexHolder = i;
            extraInfoObj.SetActive(true);
            //set info gelijk
        }

    }

    public void SwitchItemButton()
    {
        extraInfoObj.SetActive(false);
        mayMoveItem = true;
        //bool may switch
    }
}

[System.Serializable]
public class SlotInformation
{
    public Image slotImage;
    public int index;
    public int amount;
}