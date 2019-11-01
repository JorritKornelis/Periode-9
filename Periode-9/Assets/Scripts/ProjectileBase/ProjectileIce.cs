using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileIce : ProjectileBase
{
    public float moveDebuffAmount;
    public float moveDebuffTimer;
    float resetMove;
    GameObject temp;

    public GameObject impact;

    private void OnCollisionEnter(Collision mobHit)
    {
        if (mobHit.transform.tag == "Enemy")
        {
            Instantiate(impact, transform.position, Quaternion.identity);
            mobHit.transform.GetComponent<GeneralHealth>().TakeDamage(damage, mobHit.gameObject);

            resetMove = mobHit.transform.GetComponent<NavMeshAgent>().speed;
            mobHit.transform.GetComponent<NavMeshAgent>().speed -= moveDebuffAmount;

            temp = mobHit.gameObject;
            temp.GetComponent<GeneralHealth>().StartCoroutine(TimerDown(moveDebuffTimer));

            Destroy(gameObject);
        }
    }

    IEnumerator TimerDown(float f)
    {
        yield return new WaitForSeconds(f);
        temp.transform.GetComponent<NavMeshAgent>().speed = resetMove;
    }
}
