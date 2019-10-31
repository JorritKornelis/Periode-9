using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : EnemyMovementBase
{
    public float attackRadius;
    public bool activeAttack;
    public float attackTime;
    public Animator animator;
    public float damagePointOffset;
    public float damagePointSize;
    public LayerMask playerMask;
    public GameObject poof;
    public AudioSource source;
    public AudioClip[] attackClips;
    public AudioClip[] deathClips;

    public void Start()
    {
        StartFunctions();
    }

    public void Update()
    {
        if (player && Vector3.Distance(transform.position, player.transform.position) >= attackRadius && !activeAttack)
        {
            animator.SetBool("Walking", true);
            FollowPlayer();
        }
        else if (!activeAttack && player)
        {
            animator.SetBool("Walking", false);
            agent.SetDestination(transform.position);
            StartCoroutine(AttackDelay());
        }
    }

    public IEnumerator AttackDelay()
    {
        activeAttack = true;
        source.PlayOneShot(attackClips[Random.Range(0, attackClips.Length)]);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackTime);
        if (Physics.CheckSphere(transform.position + Vector3.up + (transform.forward * damagePointOffset), damagePointSize, playerMask))
        {
            StartCoroutine(Camera.main.GetComponent<ScreenShake>().Shake(0.3f));
            InflictDamage();
        }
        activeAttack = false;
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
        source.PlayOneShot(deathClips[Random.Range(0, deathClips.Length)]);
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
        Gizmos.DrawWireSphere(transform.position + Vector3.up + (transform.forward * damagePointOffset), damagePointSize);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
