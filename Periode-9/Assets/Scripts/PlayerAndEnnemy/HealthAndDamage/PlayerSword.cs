using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float swordRadius;
    public int swordDamage;
    public int acttackCoolDownTimer;

    public Animator playerDungonAnimator;

    public void Update()
    {
        PlayerSwordAtack();
    }

    //animation toevoegen
    public void PlayerSwordAtack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, swordRadius);
        int i = 0;
        if (Input.GetButtonDown("Fire1"))
        {
            playerDungonAnimator.SetBool("Attacking", true);
            Debug.Log("CUTE KITTY");

            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    Debug.Log("HIT ENEMY");
                    hitColliders[i].GetComponent<EnemyHealthScript>().TakeDamage(swordDamage, hitColliders[i].gameObject);
                    playerDungonAnimator.SetBool("Attacking", false);
                }
                i++;
            }
            StartCoroutine(AttackCoolDown());
        }
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(acttackCoolDownTimer);
        playerDungonAnimator.SetBool("Attacking", false);
    }

}
