using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float swordRadius;
    public int swordDamage;

    public void Update()
    {
        PlayerSwordAtack();
    }

    public void PlayerSwordAtack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, swordRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    Debug.Log("HIT ENEMY");
                    hitColliders[i].GetComponent<EnemyHealthScript>().TakeDamage(swordDamage, hitColliders[i].gameObject);
                }
            }
            i++;
        }
    }
}
