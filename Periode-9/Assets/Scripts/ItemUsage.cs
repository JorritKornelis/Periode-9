using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemUsage : MonoBehaviour
{
    public List<MyEvent> myEventsList = new List<MyEvent>();

    [System.Serializable]
    public class MyEvent : UnityEvent
    {


    }
    
    /*
    // potions
    public virtual void HealthPotion()
    {
        //override
    }
    public virtual void SpeedPotion()
    {
        //override
    }
    public virtual void DamagePotion()
    {
        //override
    }

    //scrolls
    public virtual void ElectricityScroll()
    {
        //override
    }
    public virtual void FireScroll()
    {
        //override
    }
    public virtual void IceScroll()
    {
        //override
    }

    //gems
    public virtual void ElectricityGem()
    {
        //override
    }
    public virtual void FireGem()
    {
        //override
    }
    public virtual void IceGem()
    {
        //override
    }

    //other
    public void RemoveItem()
    {

    }
    */
}
