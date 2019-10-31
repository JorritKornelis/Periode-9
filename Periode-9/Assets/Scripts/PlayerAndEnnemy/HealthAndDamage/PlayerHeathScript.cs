using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeathScript : GeneralHealth
{
    public void Start()
    {
        upgradeUnlocks = GameObject.FindWithTag("Manager").GetComponent<Saving>().data.unlocks;
        if (upgradeUnlocks.isUpgradeHealth == true)
        {
            maxHp = maxHpUpgrade;
        }
        else
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i >= maxHp)
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
        }
    }
}
