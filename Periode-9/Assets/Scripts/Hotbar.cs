using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    void Update()
    {
        
    }

    void HotbarFunction(int press)
    {
        if (inventory.slotInformationArray[press].index > -1)
        {
            Debug.Log("TETETETETETE");
        }
    }
}
