using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemUsage : MonoBehaviour
{
    Inventory inventoryScript;
    CharacterMovement characterMovementScript;
    PlayerHeathScript playerHeathScriptScript;
    PlayerSword playerSwordScript;

    public List<ItemUseDing> usables = new List<ItemUseDing>();

    void Start()
    {
        inventoryScript = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        playerHeathScriptScript = GameObject.FindWithTag("Player").GetComponent<PlayerHeathScript>();
        characterMovementScript = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        playerSwordScript = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerSword>();
    }

    [System.Serializable]
    public class ItemUseDing
    {
        //public int usableIndex;
        public int itemIndex;
        public UnityEvent useEvenet;
    }

    //bigpotions
    public void BigHealthPotion()
    {
        if (playerHeathScriptScript.hp < playerHeathScriptScript.maxHp)
        {
            playerHeathScriptScript.hp += 2;
            if (playerHeathScriptScript.hp > playerHeathScriptScript.maxHp)
            {
                playerHeathScriptScript.hp = playerHeathScriptScript.maxHp;
            }
        }
        else
        {
            Debug.Log("FULL HP");
        }
    }
    public void BigSpeedPotion()
    {
        float timer = 4;
        timer -= Time.deltaTime;
        float reset = characterMovementScript.moveSpeed;

        characterMovementScript.moveSpeed += 4;
        if (timer <= 0)
        {
            characterMovementScript.moveSpeed = reset;
        }
    }
    public void BigDamagePotion()
    {
        float timer = 4;
        timer -= Time.deltaTime;
        int swordReset = playerSwordScript.swordDamage;

        playerSwordScript.swordDamage += 4;
        if (timer <= 0)
        {
            playerSwordScript.swordDamage = swordReset;
        }
    }
    //small potions
    public void SmallHealthPotion()
    {
        if (playerHeathScriptScript.hp < playerHeathScriptScript.maxHp)
        {
            playerHeathScriptScript.hp += 1;
            if (playerHeathScriptScript.hp > playerHeathScriptScript.maxHp)
            {
                playerHeathScriptScript.hp = playerHeathScriptScript.maxHp;
            }
        }
        else
        {
            Debug.Log("FULL HP");
        }
    }
    public void SmallSpeedPotion()
    {
        float timer = 2;
        timer -= Time.deltaTime;
        float reset = characterMovementScript.moveSpeed;

        characterMovementScript.moveSpeed += 3;
        if (timer <= 0)
        {
            characterMovementScript.moveSpeed = reset;
        }
    }
    public void SmallDamagePotion()
    {
        float timer = 2;
        timer -= Time.deltaTime;
        int swordReset = playerSwordScript.swordDamage;

        playerSwordScript.swordDamage += 2;
        if (timer <= 0)
        {
            playerSwordScript.swordDamage = swordReset;
        }
    }
    /*

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
