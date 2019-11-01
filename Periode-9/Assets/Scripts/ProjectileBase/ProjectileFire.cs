using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : ProjectileBase
{
    public int extraFireDamage;
    int damageRes;

    public GameObject impact;

    private void OnCollisionEnter(Collision mobHit)
    {
        if (mobHit.transform.tag == "Enemy")
        {
            damageRes = damage;
            damage += extraFireDamage;

            Instantiate(impact, transform.position, Quaternion.identity);
            mobHit.transform.GetComponent<GeneralHealth>().TakeDamage(damage, mobHit.gameObject);

            damage = damageRes;

            Destroy(gameObject);
        }
    }
}
