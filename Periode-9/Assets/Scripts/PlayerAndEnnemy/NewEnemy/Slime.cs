using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyMovementBase
{
    public bool activeAttack;
    public float attackRadius;
    public Animator animator;
    public LayerMask playerMask;
    public GameObject poof;
    public float damageDelay;
    public bool allowAttack;
    public bool stopMovement;

    public void Start()
    {
        StartFunctions();
    }

    public void Update()
    {
        if (player && Vector3.Distance(transform.position, player.transform.position) >= attackRadius && !stopMovement)
        {
            animator.SetBool("Walking", true);
            FollowPlayer();
        }
        else if (player && !stopMovement)
        {
            if(allowAttack)
            {
                InflictDamage();
                StartCoroutine(Camera.main.GetComponent<ScreenShake>().Shake(0.3f));
                StartCoroutine(AttackDelay());
            }
            animator.SetBool("Walking", false);
            agent.SetDestination(transform.position);
        }
    }

    public IEnumerator AttackDelay()
    {
        allowAttack = false;
        yield return new WaitForSeconds(damageDelay);
        allowAttack = true;
    }

    public override void DeathFuntion(GameObject wichtObject)
    {
        if (!activeAttack)
        {
            int drops = Random.Range(0, dropAmount);
            for (int i = 0; i < drops; i++)
            {
                int drop = Random.Range(0, possibleDrops.Length);
                Instantiate(items.itemInformationList[drop].itemGameObject, transform.position + new Vector3(Random.Range(-dropRange, dropRange), 0f, Random.Range(-dropRange, dropRange)), Quaternion.identity);
            }
            StartCoroutine(DelayedDeath());
        }
    }

    public IEnumerator DelayedDeath()
    {
        stopMovement = true;
        agent.SetDestination(transform.position);
        activeAttack = true;
        animator.SetTrigger("Death");
        gameObject.layer = 0;
        yield return new WaitForSeconds(1f);
        Destroy(Instantiate(poof, transform.position, Quaternion.identity), 2f);
        Destroy(gameObject);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
