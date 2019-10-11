using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float swordRadius;
    public int swordDamage;
    public Vector3 swordAttackRadius;

    public float acttackCoolDownTimer;
    public Animator playerDungonAnimator;
    CharacterMovement character;

    public float damageTimer;
    public float waitForStartAttackDamage;
    public float waitForEndAttackDamage;
    public float waitForNextAnimaton;

    [Header("GemStuff")]
    public States curGem;
    public int electricSplashRange;
    public int electricSplashDamage;
    public float gemCooldown;
    public int fireDamageAmount;
    
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
        
    }

    //animation toevoegen
    public IEnumerator PlayerSwordAtack()
    {
        playerDungonAnimator.SetBool("Attacking", true);
        character.allowMovement = false;
        
        yield return new WaitForSeconds(waitForStartAttackDamage);

        Collider[] hitColliders = Physics.OverlapBox(transform.position, swordAttackRadius, transform.rotation);
        int i = 0;

        float timer = damageTimer;
        List<Collider> hitObjects = new List<Collider>();

        while (i < hitColliders.Length)
        {
            yield return null;
            timer -= Time.deltaTime;
            if (hitColliders[i].gameObject.tag == "Enemy" && !hitObjects.Contains(hitColliders[i]))
            {
                Debug.Log("HIT ENEMY");
                hitColliders[i].GetComponent<EnemyHealthScript>().TakeDamage(swordDamage, hitColliders[i].gameObject);
                hitObjects.Add(hitColliders[i]);
                
            }
            i++;
        }
        yield return new WaitForSeconds(waitForEndAttackDamage);
        
        float time = waitForNextAnimaton;
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

    public enum States
    {
        FireGem,
        IceGem,
        ElectricGem,
        None
    }

    public void SwitchGem()
    {
        switch (curGem)
        {
            case States.FireGem:
                //stuff
                int holder = swordDamage;
                swordDamage += fireDamageAmount;
                float timer = gemCooldown;
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    swordDamage = holder;
                    curGem = States.None;
                }
                break;

            case States.IceGem:
                //stuff
                Collider[] enemyIceHitColliders = Physics.OverlapSphere(transform.position, swordRadius);
                foreach (var item in enemyIceHitColliders)
                {
                    if (item.tag == "Enemy")
                    {
                        item.transform.GetComponent<EnemyHealthScript>().TakeDamage(swordDamage, item.gameObject);
                    }
                }
                curGem = States.None;
                break;

            case States.ElectricGem:
                //stuff
                Collider[] enemyHitColliders = Physics.OverlapSphere(transform.position, electricSplashRange);
                foreach (var item in enemyHitColliders)
                {
                    if (item.tag == "Enemy")
                    {
                        item.transform.GetComponent<EnemyHealthScript>().TakeDamage(electricSplashDamage, item.gameObject);
                    }
                }
                curGem = States.None;
                break;

            case States.None:
                //stuff
                Debug.Log("NO EFFECT");
                break;

        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(gemCooldown);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, swordAttackRadius);
        
    }

}
