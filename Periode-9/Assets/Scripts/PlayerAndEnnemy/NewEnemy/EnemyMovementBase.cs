using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementBase : GeneralHealth
{
    public string playerTag;
    public GameObject player;
    public NavMeshAgent agent;
    public int damage;

    public virtual void StartFunctions()
    {
        player = GameObject.FindWithTag(playerTag);
    }

    public virtual void FollowPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public void InflictDamage()
    {
        player.GetComponent<PlayerHeathScript>().TakeDamage(damage, player);
    }
}
