using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileIce : ProjectileBase
{
    public float moveDebuffAmount;
    public float moveDebuffTimer;

    private void OnCollisionEnter(Collision mobHit)
    {
        if (mobHit.transform.tag == "Enemy")
        {
            mobHit.transform.GetComponent<EnemyHealthScript>().TakeDamage(damage, mobHit.gameObject);

            float restetMove = mobHit.transform.GetComponent<EnemyMovement>().moveSpeed;
            mobHit.transform.GetComponent<EnemyMovement>().moveSpeed -= moveDebuffAmount;

            float timer = moveDebuffTimer;
            timer -= Time.deltaTime;
            if (moveDebuffTimer <= 0)
            {
                mobHit.transform.GetComponent<EnemyMovement>().moveSpeed = restetMove;
            }
        }
    }
}
