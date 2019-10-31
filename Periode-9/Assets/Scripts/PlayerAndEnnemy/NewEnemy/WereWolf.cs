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
    public GameObject damageParticle;

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
            Debug.Log("Stop");
            agent.SetDestination(transform.position);
            StartCoroutine(AttackDelay());
        }
    }

    public IEnumerator AttackDelay()
    {
        animator.SetTrigger("Attack");
        activeAttack = true;
        float time = attackTime;
        Destroy(Instantiate(damageParticle, transform.position + (transform.forward * damagePointOffset), Quaternion.identity), 2f);
        while(time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        activeAttack = false;
    }
}
