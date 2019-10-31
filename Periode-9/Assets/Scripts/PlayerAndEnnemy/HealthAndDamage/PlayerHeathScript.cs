using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeathScript : GeneralHealth
{
    public int maxHpUpgrade;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [HideInInspector]
    public UpgradeUnlocks upgradeUnlocks;

    public override void TakeDamage(int damageAmount, GameObject witchObject)
    {
        base.TakeDamage(damageAmount, witchObject);
        SetHealthDisplay();
    }

    public void SetHealthDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHp)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

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
