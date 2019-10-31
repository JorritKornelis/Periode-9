using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float swordRadius;
    public int swordDamage;
    public int upgradeSwordDamage;
    public Vector3 swordAttackRadius;
    public float acttackCoolDownTimer;
    public Animator playerDungonAnimator;
    CharacterMovement character;
    public float damageTimer;
    public float waitForNextAnimaton;
    UpgradeUnlocks upgradeUnlocks;

    [Header("GemStuff")]
    public States curGem;
    public int electricSplashRange;
    public int electricSplashDamage;
    public int gemTiks;
    public int fireDamageAmount;
    public Material firemat;
    public Material iceMat;
    public Material ecMat;
    public Material resetMat;
    public GameObject swordModel;

    void Start()
    {
        character = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();

        swordModel.GetComponent<Renderer>().material = resetMat;

        upgradeUnlocks = GameObject.FindWithTag("Manager").GetComponent<Saving>().data.unlocks;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerDungonAnimator.GetBool("Attacking") == false && character.allowMovement == true)
        {
            StartCoroutine(PlayerSwordAtack());
        }
        SwitchGem();//
    }

    //animation toevoegen
    public IEnumerator PlayerSwordAtack()
    {
        playerDungonAnimator.SetBool("Attacking", true);
        character.allowMovement = false;
        //playerDungonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking");
        List<GameObject> hitObjects = new List<GameObject>();
        float timer = damageTimer;

        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
            Collider[] hitColliders = Physics.OverlapBox(transform.position, swordAttackRadius, Quaternion.identity);
            int i = 0;

            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "Enemy" && !hitObjects.Contains(hitColliders[i].gameObject))
                {
                    Debug.Log("HIT ENEMY");
                    hitObjects.Add(hitColliders[i].gameObject);
                    if (upgradeUnlocks.isUpgradeSword == true)
                    {
                        hitColliders[i].GetComponent<GeneralHealth>().TakeDamage(upgradeSwordDamage, hitColliders[i].gameObject);
                    }
                    else
                    {
                        hitColliders[i].GetComponent<GeneralHealth>().TakeDamage(swordDamage, hitColliders[i].gameObject);
                    }
                }
                i++;
            }
            
            hitObjects.Clear();
        }   
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
                swordModel.GetComponent<Renderer>().material = firemat;

                int holder = swordDamage;
                swordDamage += fireDamageAmount;

                if (Input.GetButtonDown("Fire1"))
                    gemTiks -= 1;

                if (gemTiks <= 0)
                {
                    swordDamage = holder;
                    curGem = States.None;
                }
                break;

            case States.IceGem:
                //stuff
                swordModel.GetComponent<Renderer>().material = iceMat;

                if (Input.GetButtonDown("Fire1"))
                {
                    Collider[] enemyIceHitColliders = Physics.OverlapSphere(transform.position, swordRadius);
                    foreach (var item in enemyIceHitColliders)
                    {
                        if (item.tag == "Enemy")
                        {
                            item.transform.GetComponent<GeneralHealth>().TakeDamage(swordDamage, item.gameObject);
                        }
                    }
                    curGem = States.None;
                }

                break;

            case States.ElectricGem:
                //stuff
                swordModel.GetComponent<Renderer>().material = ecMat;

                if (Input.GetButtonDown("Fire1"))
                {
                    Collider[] enemyHitColliders = Physics.OverlapSphere(transform.position, electricSplashRange);
                    foreach (var item in enemyHitColliders)
                    {
                        if (item.tag == "Enemy")
                        {
                            item.transform.GetComponent<GeneralHealth>().TakeDamage(electricSplashDamage, item.gameObject);
                        }
                    }
                    curGem = States.None;
                }

                break;

            case States.None:
                
                swordModel.GetComponent<Renderer>().material = resetMat;
                Debug.Log("NO EFFECT");

                break;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, swordAttackRadius);
    }
}
