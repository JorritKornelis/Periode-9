using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileElecticity : ProjectileBase
{
    public float radius;
    public int splashDamage;

    public GameObject impact;

    private void OnCollisionEnter(Collision mobHit)
    {
        if (mobHit.transform.tag == "Enemy")
        {
            mobHit.transform.GetComponent<GeneralHealth>().TakeDamage(damage, mobHit.gameObject);
            Instantiate(impact, transform.position, Quaternion.identity);

            /*Collider[] enemyHitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var item in enemyHitColliders)
            {
                if (item.tag == "Enemy")
                {
                    item.transform.GetComponent<GeneralHealth>().TakeDamage(splashDamage, mobHit.gameObject);
                }
            }*/

        }
    }
}
