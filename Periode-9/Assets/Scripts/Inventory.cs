using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject invObj;

    public ItemClassScriptableObject itemScriptableObject;
    public SlotInformation[] slotInformationArray;

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            invObj.SetActive(!invObj.activeSelf);
        }
        foreach (SlotInformation slot in slotInformationArray)
        {
            slot.slotImage.sprite = itemScriptableObject.itemInformationList[slot.index].Sprite;
        }
    }

    void ChangeItemLocation(int i)
    {
        if (slotInformationArray[i].slotImage != null)
        {
            slotInformationArray[i].selected = true;
        }
        SlotInformation slotTemp = slotInformationArray[i];

        if (slotInformationArray[i].slotImage == null)
        {
            foreach (SlotInformation s in slotInformationArray)
            {
                if (s.selected == true)
                {
                    slotInformationArray[i] = slotTemp;
                }
                else
                {
                    Debug.Log("PEnisPRIKKER");
                }
            }
        }
    }
}

[System.Serializable]
public class SlotInformation
{
    public Image slotImage;
    public int index;
    public int amount;
    public bool selected;
}