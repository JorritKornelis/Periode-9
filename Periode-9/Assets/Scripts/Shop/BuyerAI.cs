using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public CharacterStatistics stats;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public BuyerSpawner spawnerInfo;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Vector2 min = spawnerInfo.minRandomPoint;
        Vector2 max = spawnerInfo.maxRandomPoint;
        StartCoroutine(MoveToPoint(new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y))));
    }

    public IEnumerator MoveToPoint(Vector2 movePoint)
    {
        yield return null;
        Vector3 aiPoint = new Vector3(movePoint.x, spawnerInfo.height, movePoint.y);
        agent.SetDestination(aiPoint);

        while (Vector3.Distance(transform.position, aiPoint) > 0.5f)
            yield return null;

        yield return new WaitForSeconds(stats.waitTime);
        float randomBuyChance = Random.Range(0, 100);
        if (randomBuyChance > stats.buyCheckChance)
        {
            Vector2 min = spawnerInfo.minRandomPoint;
            Vector2 max = spawnerInfo.maxRandomPoint;
            StartCoroutine(MoveToPoint(new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y))));
        }
    }

    public void SetVisuals()
    {
        meshFilter.mesh = stats.characterMesh;
        meshRenderer.material = stats.characterMaterial;
    }
}
