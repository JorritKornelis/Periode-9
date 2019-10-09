using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHealth : MonoBehaviour
{
    public int hp;
    public int maxHp;

    public virtual void TakeDamage(int damageAmount,GameObject witchObject)
    {
        //overload slowAmount
        //if GetcomponetEnemyhealth /slowspeed

        hp -= damageAmount;
        if (hp <= 0)
        {
            Debug.Log("Killed " + witchObject.name);
            DeathFuntion(witchObject);
        }
    }
    public virtual void DeathFuntion(GameObject wichtObject)
    {
        Destroy(wichtObject);
    }
}
