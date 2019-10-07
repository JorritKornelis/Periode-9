using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : ProjectileBase
{
    public int fireDamage;

    private void OnCollisionEnter(Collision mobHit)
    {
        if (mobHit.transform.tag == "Enemy")
        {
            damage += fireDamage;
            
            mobHit.transform.GetComponent<EnemyHealthScript>().TakeDamage(damage, mobHit.gameObject);
        }
    }
}
