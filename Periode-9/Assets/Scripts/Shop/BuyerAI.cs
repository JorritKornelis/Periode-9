using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuyerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public CharacterStatistics stats;
    public SkinnedMeshRenderer meshRenderer;
    public BuyerSpawner spawnerInfo;
    public IEnumerator currentCoroutine;
    public Transform itemDisplay;
    public bool hasItem;
    public float rotateSpeed;
    public int currentTries;
    public bool activeSoundClip;
    public AudioSource audiosource;
    public AudioClip[] clips;
    public AudioClip cashSound;
    [Range(0,100)]
    public float talkChance;
    [Range(0,1)]
    public float pitchRange;
    public Animator animator;

    public int currentItem = -1;
    public int amount;
    public float setPrice;

    public IEnumerator PlayRandomAudioClip()
    {
        float chance = Random.Range(0, 100);
        if (!activeSoundClip && chance < talkChance)
        {
            audiosource.pitch = 1f + Random.Range(-pitchRange, pitchRange);
            activeSoundClip = true;
            int index = Random.Range(0, clips.Length);
            audiosource.PlayOneShot(clips[index]);
            yield return new WaitForSeconds(clips[index].length);
            activeSoundClip = false;
        }
    }

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(IdleMove());
    }

    public IEnumerator IdleMove()
    {
        StartCoroutine(PlayRandomAudioClip());
        Vector2 min = spawnerInfo.minRandomPoint;
        Vector2 max = spawnerInfo.maxRandomPoint;
        Vector2 movePoint = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        yield return null;
        Vector3 aiPoint = new Vector3(movePoint.x, spawnerInfo.height, movePoint.y);
        agent.SetDestination(aiPoint);
        animator.SetBool("Walking", true);
        while (Vector3.Distance(transform.position, aiPoint) > 0.5f)
            yield return null;
        animator.SetBool("Walking", false);

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
            currentTries++;
            if (currentTries > stats.leaveChance)
                StartCoroutine(LeaveStore());
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
        animator.SetBool("Walking", true);
        while (Vector3.Distance(transform.position, possibleBuyable[index].transform.position) > 0.5f)
        {
            Debug.DrawLine(transform.position, possibleBuyable[index].transform.position,Color.red);
            yield return null;
        }
        animator.SetBool("Walking", false);

        Vector3 dir = new Vector3(possibleBuyable[index].itemDisplay.position.x, transform.position.y, possibleBuyable[index].itemDisplay.position.z) - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir, transform.up);

        while (Quaternion.Angle(transform.rotation, lookRot) > 0.2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * rotateSpeed);
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
        currentItem = possibleBuyable[index].item;
        amount = possibleBuyable[index].amount;
        setPrice = possibleBuyable[index].sellPrice;
        possibleBuyable[index].item = -1;
        possibleBuyable[index].amount = 0;
        possibleBuyable[index].lookedAt = false;
        hasItem = true;
        possibleBuyable[index].DisplayItem();
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
        animator.SetBool("Walking", true);
        while (Vector3.Distance(transform.position, spawnerInfo.counterLocation.position) > 0.5f)
        {
            Debug.DrawLine(transform.position, spawnerInfo.counterLocation.position, Color.red);
            yield return null;
        }
        animator.SetBool("Walking", false);

        Vector3 dir = (new Vector3(spawnerInfo.counterLocation.position.x,transform.position.y, spawnerInfo.counterLocation.position.z) + Vector3.forward) - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir, transform.up);

        while (Quaternion.Angle(transform.rotation, lookRot) > 0.5f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * rotateSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        GameObject.FindWithTag(spawnerInfo.managerTag).GetComponent<Saving>().data.currency += Mathf.RoundToInt(setPrice * amount);
        spawnerInfo.counterAvailable = true;
        audiosource.PlayOneShot(cashSound, 2);
        StartCoroutine(LeaveStore());
    }
    public IEnumerator LeaveStore()
    {
        animator.SetBool("Walking", true);
        foreach (Transform child in itemDisplay)
            Destroy(child.gameObject);
        agent.SetDestination(spawnerInfo.doorLocation.position);
        yield return null;
        while (Vector3.Distance(transform.position, spawnerInfo.doorLocation.position) > 0.5f)
        {
            Debug.DrawLine(transform.position, spawnerInfo.doorLocation.position, Color.red);
            yield return null;
        }
        spawnerInfo.currentBuyers--;
        Destroy(gameObject);
    }

    public void SetVisuals()
    {
        meshRenderer.material = stats.characterMaterial;
    }
}
