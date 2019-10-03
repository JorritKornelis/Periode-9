using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemUsage : MonoBehaviour
{
    public Inventory inventoryScript;

    public List<ItemUseDing> usables = new List<ItemUseDing>();

    void Start()
    {
        inventoryScript = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    public void SelectUseItem()
    {

    }

    [System.Serializable]
    public class ItemUseDing
    {
        public int usableIndex;
        public int itemIndex;
        public UnityEvent useEvenet;
    }


    /*
    // potions
    public void HealthPotion()
    {
        //override
    }
    public void SpeedPotion()
    {
        //override
    }
    public void DamagePotion()
    {
        //override
    }

    //scrolls
    public void ElectricityScroll()
    {
        //override
    }
    public void FireScroll()
    {
        //override
    }
    public void IceScroll()
    {
        //override
    }

    //gems
    public void ElectricityGem()
    {
        //override
    }
    public void FireGem()
    {
        //override
    }
    public void IceGem()
    {
        //override
    }

    //other
    public void RemoveItem()
    {

    }
    */
}
