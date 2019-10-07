using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public float moveSpeed;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update()
    {
        AiMovement();
    }

    public void AiMovement()
    {
        agent.destination = target.position;
    }
}
