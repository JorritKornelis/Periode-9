using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralHealth : MonoBehaviour
{
    public int hp;
    public int maxHp;

    Saving sav;
    public GameObject respawnUi;

    private void Awake()
    {
        respawnUi.SetActive(false);
        sav = GameObject.FindWithTag("Manager").GetComponent<Saving>();
    }

    public virtual void TakeDamage(int damageAmount, GameObject witchObject)
    {
        hp -= damageAmount;

        if (hp <= 0)
        {
            if (witchObject.tag == "Player")
            {
                Debug.Log("Killed " + witchObject.name);
                RespawnPlayerUi();
                //DeathFuntion(witchObject);
            }
            else
            {
                Debug.Log("Killed " + witchObject.name);
                DeathFuntion(witchObject);
            }
        }
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

    public virtual void DeathFuntion(GameObject wichtObject)
    {
        Destroy(wichtObject);
    }
}
