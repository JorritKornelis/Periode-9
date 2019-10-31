using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeathScript : GeneralHealth
{
    public int maxHpUpgrade;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [HideInInspector]
    public UpgradeUnlocks upgradeUnlocks;

    Saving sav;
    public GameObject respawnUi;

    private void Awake()
    {
        respawnUi.SetActive(false);
        sav = GameObject.FindWithTag("Manager").GetComponent<Saving>();
    }

    public void RespawnPlayerUi()
    {
        respawnUi.SetActive(true);
        Time.timeScale = 0;
    }

    public void RespawnButton()
    {
        sav.data.inventory.Clear();
        sav.SaveData();

        SceneManager.LoadScene("ShopRoomScene");
        respawnUi.SetActive(false);
        Time.timeScale = 1;
    }

    public void RespawnBackToMenuButton()
    {
        sav.data.inventory.Clear();
        sav.SaveData();

        SceneManager.LoadScene("MainMenu");
        respawnUi.SetActive(false);
        Time.timeScale = 1;
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

    public override void TakeDamage(int damageAmount, GameObject witchObject)
    {
        base.TakeDamage(damageAmount, witchObject);
        SetHealthDisplay();
        if(hp <= 0)
            RespawnPlayerUi();
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
}
