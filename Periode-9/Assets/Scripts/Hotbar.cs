﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    Inventory inventoryScript;
    public ItemUsage itemUsageScript;

    void Start()
    {
        inventoryScript = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    void Update()
    {
        for (int i = 1; i <= 5; i++)
            if (Input.GetButtonDown(i.ToString()))
                HotbarFunction(i - 1);
    }

    public void HotbarFunction(int press)
    {
        if (inventoryScript.slotInformationArray[press].index > -1)
            foreach (ItemUsage.ItemUseDing item in itemUsageScript.usables)
                if (item.itemIndex == inventoryScript.slotInformationArray[press].index)
                {
                    Debug.Log(press + " HOTBSR PRESS");
                    item.useEvenet.Invoke();
                    break;
                }
    }
}
