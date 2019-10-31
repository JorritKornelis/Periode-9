using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WereWolf : EnemyMovementBase
{
    public float attackRadius;

    public void Start()
    {
        StartFunctions();
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= attackRadius)
            FollowPlayer();
    }
}
