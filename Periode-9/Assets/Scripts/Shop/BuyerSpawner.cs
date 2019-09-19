using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerSpawner : MonoBehaviour
{
    public Vector2 minRandomPoint, maxRandomPoint;
    public float height;
    public CharacterStatistics[] buyerStats;
    public Transform doorLocation, counterLocation;
    public int maxBuyers;
    public GameObject buyerBaseObject;
    private int currentBuyers;
    public float spawnTimer;

    public void Start()
    {
        StartCoroutine(Spawner());
    }

    public IEnumerator Spawner()
    {
        while (true)
        {
            if (currentBuyers < maxBuyers)
            {
                BuyerAI buyer = Instantiate(buyerBaseObject, doorLocation.position, doorLocation.rotation).GetComponent<BuyerAI>();
                buyer.stats = buyerStats[Random.Range(0, buyerStats.Length)];
                buyer.SetVisuals();
                buyer.spawnerInfo = this;
                currentBuyers++;
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(minRandomPoint.x, height, minRandomPoint.y), Vector3.one * 0.3f);
        Gizmos.DrawCube(new Vector3(maxRandomPoint.x, height, maxRandomPoint.y), Vector3.one * 0.3f);
    }
}
