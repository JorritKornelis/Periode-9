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
    public IEnumerator currentCoroutine;
    public Transform itemDisplay;
    public bool hasItem;
    public bool counter;

    public int currentItem = -1;
    public int amount;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(IdleMove());
    }

    public IEnumerator IdleMove()
    {
        Vector2 min = spawnerInfo.minRandomPoint;
        Vector2 max = spawnerInfo.maxRandomPoint;
        Vector2 movePoint = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        yield return null;
        Vector3 aiPoint = new Vector3(movePoint.x, spawnerInfo.height, movePoint.y);
        agent.SetDestination(aiPoint);

        while (Vector3.Distance(transform.position, aiPoint) > 0.5f)
            yield return null;

        yield return new WaitForSeconds(stats.waitTime);
        if (hasItem)
        {
            if(spawnerInfo.counterAvailable)
                StartCoroutine(WaitingAtCounter());
            else
                StartCoroutine(IdleMove());
        }
        else
        {
            float randomBuyChance = Random.Range(0, 100);
            if (randomBuyChance > stats.buyCheckChance)
                StartCoroutine(IdleMove());
            else
            {
                currentCoroutine = BuyItem();
                StartCoroutine(currentCoroutine);
            }
        }
    }

    public IEnumerator BuyItem()
    {
        List<SellPoint> possibleBuyable = new List<SellPoint>();
        foreach (SellPoint point in spawnerInfo.sellPoints)
            if(point.item != -1 && !point.lookedAt)
            {
                int interested = Random.Range(0, 100);
                if (interested < stats.otherThanInterestedChance)
                    possibleBuyable.Add(point);
                else if (stats.interestedItems.Contains(point.item))
                    possibleBuyable.Add(point);
            }
        if(possibleBuyable.Count == 0)
        {
            StartCoroutine(IdleMove());
            StopCoroutine(currentCoroutine);
        }
        yield return null;
        int index = Random.Range(0, possibleBuyable.Count);
        possibleBuyable[index].lookedAt = true;  

        agent.SetDestination(possibleBuyable[index].transform.position);
        while (Vector3.Distance(transform.position, possibleBuyable[index].transform.position) > 0.5f)
        {
            Debug.DrawLine(transform.position, possibleBuyable[index].transform.position,Color.red);
            yield return null;
        }

        Vector3 dir = new Vector3(possibleBuyable[index].itemDisplay.position.x, transform.position.y, possibleBuyable[index].itemDisplay.position.z) - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir, transform.up);

        while (Quaternion.Angle(transform.rotation, lookRot) > 0.2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 4f);
            yield return null;
        }

        float buyChance = Random.Range(0, 100);
        if (buyChance > stats.buyChance)
        {
            StartCoroutine(IdleMove());
            possibleBuyable[index].lookedAt = false;
            StopCoroutine(currentCoroutine);
        }
        yield return null;
        GameObject g = Instantiate(spawnerInfo.items.itemInformationList[possibleBuyable[index].item].itemGameObject, itemDisplay.position, itemDisplay.rotation, itemDisplay);
        g.GetComponent<ItemIndex>().enabled = false;
        g.layer = 0;
        hasItem = true;
        if (spawnerInfo.counterAvailable)
            StartCoroutine(WaitingAtCounter());
        else
            StartCoroutine(IdleMove());
    }

    public IEnumerator WaitingAtCounter()
    {
        spawnerInfo.counterAvailable = false;
        agent.SetDestination(spawnerInfo.counterLocation.position);
        yield return null;
        while (Vector3.Distance(transform.position, spawnerInfo.counterLocation.position) > 0.5f)
        {
            Debug.DrawLine(transform.position, spawnerInfo.counterLocation.position, Color.red);
            yield return null;
        }

        Vector3 dir = (spawnerInfo.counterLocation.position + Vector3.forward) - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir, transform.up);

        while (Quaternion.Angle(transform.rotation, lookRot) > 0.2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 4f);
            yield return null;
        }

    }

    public void SetVisuals()
    {
        meshFilter.mesh = stats.characterMesh;
        meshRenderer.material = stats.characterMaterial;
    }
}
