using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraInfoSlot : MonoBehaviour
{
    Inventory inventory;

    public bool trueIsInv;

    void Awake()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    public void ItemMove(int index)
    {
        if (trueIsInv)
        {
            Debug.Log("GAGADVYAFVuihufgggfhwuifguwgfuwfbwyhfhqui   1");
            //inventory.ItemMove(index, inventory.slotInformationArray);
        }
        else
        {
            Debug.Log("GAGADVYAFVuihufgggfhwuifguwgfuwfbwyhfhqui   2");
            //inventory.ItemMove(index, inventory.storageSystemHolder.chestSlotArray);
        }
    }
}
