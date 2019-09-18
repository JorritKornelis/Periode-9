using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float waitTime;
    [Range(0, 100)]
    public float buyChance;
    public Vector2 minRandomPoint, maxRandomPoint;
    public float height;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MoveToPoint(new Vector2(Random.Range(minRandomPoint.x, maxRandomPoint.x), Random.Range(minRandomPoint.y, maxRandomPoint.y))));
    }

    public IEnumerator MoveToPoint(Vector2 movePoint)
    {
        yield return null;
        Vector3 aiPoint = new Vector3(movePoint.x, height, movePoint.y);
        agent.SetDestination(aiPoint);

        while (Vector3.Distance(transform.position, aiPoint) > 0.2f)
            yield return null;

        yield return new WaitForSeconds(waitTime);
        float randomBuyChance = Random.Range(0, 100);
        if (randomBuyChance > buyChance)
            StartCoroutine(MoveToPoint(new Vector2(Random.Range(minRandomPoint.x, maxRandomPoint.x), Random.Range(minRandomPoint.y, maxRandomPoint.y))));
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(minRandomPoint.x, height, minRandomPoint.y), Vector3.one * 0.3f);
        Gizmos.DrawCube(new Vector3(maxRandomPoint.x, height, maxRandomPoint.y), Vector3.one * 0.3f);
    }
}
