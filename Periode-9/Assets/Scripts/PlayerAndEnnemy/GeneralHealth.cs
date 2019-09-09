using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHealth : MonoBehaviour
{
    public int hp;

    public int damage;

    public virtual void TakeDamage()
    {
        hp -= damage;
        if (hp - damage <= 0)
        {
            Debug.Log("YOU DIED");
            DeathFuntion();
        }
    }
    public virtual void DeathFuntion()
    {
        //override
    }
}
