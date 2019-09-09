using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRadius;
    public int attackDamage;

    void Update()
    {
        EnemyMeleeAttack();
    }

    public void EnemyMeleeAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Player")
            {
                Debug.Log("HIT ENEMY");
                hitColliders[i].GetComponent<EnemyHealthScript>().TakeDamage(attackDamage, hitColliders[i].gameObject);
            }
            i++;
        }
    }
}
