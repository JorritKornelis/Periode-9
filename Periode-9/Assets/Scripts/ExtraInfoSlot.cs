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

    public void NeedToSave(int index)
    {
        inventory.SelectItem(index, trueIsInv);
    }
}
