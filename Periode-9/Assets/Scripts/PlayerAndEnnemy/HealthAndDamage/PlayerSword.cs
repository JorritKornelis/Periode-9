using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float swordRadius;
    public int swordDamage;
    public float acttackCoolDownTimer;

    public Animator playerDungonAnimator;
    bool maySecondA = false;

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
                //if nog een keer input
                if (Input.GetButtonDown("Fire1"))
                {
                    //ga naar 2de
                    if (playerDungonAnimator.GetBool("Attack2") == false)
                    {
                        maySecondA = true;   
                    }
                }
                //dan cooldown
                if (maySecondA == true)
                {
                    StartCoroutine(AttackCoolDown());
                }
                if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    Debug.Log("HIT ENEMY");
                    hitColliders[i].GetComponent<EnemyHealthScript>().TakeDamage(swordDamage, hitColliders[i].gameObject);
                    playerDungonAnimator.SetBool("Attacking", false);
                }
                i++;
            }
            if (maySecondA == false)
            {
                StartCoroutine(AttackCoolDown());
            }
        }
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(acttackCoolDownTimer);
        playerDungonAnimator.SetBool("Attacking", false);
        maySecondA = false;
    }

}
