using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralHealth : MonoBehaviour
{
    public int hp;
    public int maxHp;

    
    public virtual void TakeDamage(int damageAmount, GameObject witchObject)
    {
        hp -= damageAmount;

        if (hp <= 0)
        {
            if (witchObject.tag == "Player")
            {
                Debug.Log("Killed " + witchObject.name);
                //DeathFuntion(witchObject);
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
