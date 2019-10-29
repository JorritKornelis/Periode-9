using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRadius;
    public int attackDamage;
    public float animationTime;
    bool mayHit = true;

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
            if (hitColliders[i].gameObject.tag == "Player" && mayHit == true)
            {
                mayHit = false;
                StartCoroutine(DamageTimer());
            }
            i++;
        }
    }

    public IEnumerator DamageTimer()
    {
        //start animation
        yield return new WaitForSeconds(animationTime);
        //end animation
        mayHit = true;
        float dis = Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
        if (dis < attackRadius)
        {
            Debug.Log("HIT Player");
            GameObject.FindWithTag("Player").GetComponent<PlayerHeathScript>().TakeDamage(attackDamage, GameObject.FindWithTag("Player"));
        }
    }

    /*public void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, attackRadius);

    }
    */
}
