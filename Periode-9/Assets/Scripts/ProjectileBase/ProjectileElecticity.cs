using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileElecticity : ProjectileBase
{
    public float radius;
    public int splashDamage;

    private void OnCollisionEnter(Collision mobHit)
    {
        if (mobHit.transform.tag == "Enemy")
        {
            mobHit.transform.GetComponent<EnemyHealthScript>().TakeDamage(damage, mobHit.gameObject);

            Collider[] enemyHitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var item in enemyHitColliders)
            {
                if (item.tag == "Enemy")
                {
                    item.transform.GetComponent<EnemyHealthScript>().TakeDamage(splashDamage, mobHit.gameObject);
                }
            }

        }
    }
}
