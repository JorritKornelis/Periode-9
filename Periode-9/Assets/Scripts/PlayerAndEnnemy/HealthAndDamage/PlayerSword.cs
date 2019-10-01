using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float swordRadius;
    public int swordDamage;

    public float acttackCoolDownTimer;
    public Animator playerDungonAnimator;
    CharacterMovement character;

    void Start()
    {
        character = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerDungonAnimator.GetBool("Attacking") == false)
        {
            StartCoroutine(PlayerSwordAtack());
        }
        PlayerSwordAtack();
    }

    //animation toevoegen
    public IEnumerator PlayerSwordAtack()
    {
        playerDungonAnimator.SetBool("Attacking", true);
        character.allowMovement = false;

        yield return new WaitForSeconds(0.1f);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, swordRadius);
        int i = 0;

        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                Debug.Log("HIT ENEMY");
                hitColliders[i].GetComponent<EnemyHealthScript>().TakeDamage(swordDamage, hitColliders[i].gameObject);
            }
            i++;
        }
        yield return new WaitForSeconds(0.1f);
        float time = 0.1f;
        bool nextAttack = false;
        while (time > 0)
        {
            yield return null;
            time -= Time.deltaTime;
            if (Input.GetButtonDown("Fire1"))
            {
                nextAttack = true;
            }
        }
        if (nextAttack)
        {
            StartCoroutine(PlayerSwordAtack());
        }
        else
        {
            playerDungonAnimator.SetBool("Attacking", false);
            character.allowMovement = true;
        }
    }
}
