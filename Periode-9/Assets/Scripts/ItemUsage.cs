using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemUsage : MonoBehaviour
{
    GameObject playerGameobject;
    Inventory inventoryScript;
    CharacterMovement characterMovementScript;
    PlayerHeathScript playerHeathScriptScript;
    PlayerSword playerSwordScript;
    public List<ItemUseDing> usables = new List<ItemUseDing>();

    [Header("Public Varibels")]
    public float speedTimer;
    public float damageUpTimer;

    void Start()
    {
        playerGameobject = GameObject.FindWithTag("Player");
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

    //Potions
    public void HealthPotions(int healAmount)
    {
        if (playerHeathScriptScript.hp < playerHeathScriptScript.maxHp)
        {
            playerHeathScriptScript.hp += healAmount;
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
    public void SpeedPotions(int speedAmount)
    {
        float timer = speedTimer;
        timer -= Time.deltaTime;
        float reset = characterMovementScript.moveSpeed;

        characterMovementScript.moveSpeed += speedAmount;
        if (timer <= 0)
        {
            characterMovementScript.moveSpeed = reset;
        }
    }
    public void DamagePotions(int damageUpAmount)
    {
        float timer = damageUpTimer;
        timer -= Time.deltaTime;
        int swordReset = playerSwordScript.swordDamage;

        playerSwordScript.swordDamage += damageUpAmount;
        if (timer <= 0)
        {
            playerSwordScript.swordDamage = swordReset;
        }
    }

    //scrolls
    public void UseScrolls(GameObject ScrollObj)
    {
        Instantiate(ScrollObj, playerGameobject.transform.position, characterMovementScript.body.rotation);
        //electricityScrollObj
        //FireScrollObj
        //IceScrollObj
    }

    //Gems
    
}
