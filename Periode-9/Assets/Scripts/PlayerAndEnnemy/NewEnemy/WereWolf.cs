using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolf : EnemyMovementBase
{
    public float attackRadius;
    public bool activeAttack;
    public float attackTime;
    public Animator animator;
    public float damagePointOffset;
    public float damagePointSize;
    public float attackStartDelay;
    public GameObject damageParticle;
    public LayerMask playerMask;

    public void Start()
    {
        StartFunctions();
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= attackRadius && !activeAttack)
        {
            animator.SetBool("Walking", true);
            FollowPlayer();
        }
        else if(!activeAttack)
        {
            animator.SetBool("Walking", false);
            agent.SetDestination(transform.position);
            StartCoroutine(AttackDelay());
        }
    }

    public IEnumerator AttackDelay()
    {
        activeAttack = true;
        yield return new WaitForSeconds(attackStartDelay);
        animator.SetTrigger("Attack");
        float time = attackTime / 2f;
        Destroy(Instantiate(damageParticle, transform.position + Vector3.up + transform.right * 0.4f + (transform.forward * damagePointOffset), transform.rotation), 2f);
        if (Physics.CheckSphere(transform.position + Vector3.up + (transform.forward * damagePointOffset), damagePointSize, playerMask))
            InflictDamage();
        yield return new WaitForSeconds(attackTime / 2f);
        time = attackTime / 2f;
        Destroy(Instantiate(damageParticle, transform.position + Vector3.up + transform.right * -0.4f + (transform.forward * damagePointOffset), transform.rotation), 2f);
        if (Physics.CheckSphere(transform.position + Vector3.up + (transform.forward * damagePointOffset), damagePointSize, playerMask))
            InflictDamage();
        yield return new WaitForSeconds(attackTime / 2f);
        activeAttack = false;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up + (transform.forward * damagePointOffset), damagePointSize);
    }
}
