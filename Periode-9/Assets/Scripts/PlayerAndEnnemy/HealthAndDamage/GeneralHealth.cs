using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralHealth : MonoBehaviour
{
    public int hp;
    public int maxHp;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public virtual void TakeDamage(int damageAmount,GameObject witchObject)
    {
        hp -= damageAmount;
        if (witchObject.tag == "Player")
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
                if (i<maxHp)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }

        if (hp <= 0)
        {
            if (witchObject.tag == "Player")
            {
                Debug.Log("Killed " + witchObject.name);
                //respawn
                DeathFuntion(witchObject);
            }
            else
            {
                Debug.Log("Killed " + witchObject.name);
                DeathFuntion(witchObject);
            }

        }
    }

    public virtual void DeathFuntion(GameObject wichtObject)
    {
        Destroy(wichtObject);
    }
}
