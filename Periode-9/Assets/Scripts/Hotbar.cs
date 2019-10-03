using System.Collections;
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
        if (Input.GetButtonDown("1"))
        {
            HotbarFunction(1);
        }
        if (Input.GetButtonDown("2"))
        {
            HotbarFunction(2);
        }
        if (Input.GetButtonDown("3"))
        {
            HotbarFunction(3);
        }
        if (Input.GetButtonDown("4"))
        {
            HotbarFunction(4);
        }
        if (Input.GetButtonDown("5"))
        {
            HotbarFunction(5);
        }
    }

    public void HotbarFunction(int press)
    {
        int i;
        i = press - 1;
        if (inventoryScript.slotInformationArray[i].index > -1)
        {
            if (inventoryScript.itemScriptableObject.itemInformationList[inventoryScript.slotInformationArray[i].index].canUse == true)
            {
                itemUsageScript.SelectUseItem();
                //
            }
        }
    }
}
