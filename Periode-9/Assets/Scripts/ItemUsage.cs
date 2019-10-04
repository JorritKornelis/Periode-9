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

    public void SelectUseItem(int item)
    {
        //usables[item] 

    }

    [System.Serializable]
    public class ItemUseDing
    {
        //public int usableIndex;
        public int itemIndex;
        public UnityEvent useEvenet;
    }

    // potions
    public void BigHealthPotion()
    {
        Debug.Log("BIG HP POTION");
    }
    /*
    public void BigSpeedPotion()
    {
        //override
    }
    public void BigDamagePotion()
    {
        //override
    }

    public void SmallHealthPotion()
    {
        Debug.Log("BIG HP POTION");
    }

    public void SmallSpeedPotion()
    {
        Debug.Log("BIG HP POTION");
    }
    
    public void SmallDamagePotion()
    {
        Debug.Log("BIG HP POTION");
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
