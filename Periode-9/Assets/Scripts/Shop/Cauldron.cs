using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public CombinationInfo[] combinations;
    public int item1, item2;
    public int craftedItem;
    public bool check;
    public GameObject accepted, rejected;
    public float particleLifetime;

    public void Update()
    {
        if (check)
        {
            CheckForCrafting();
            check = false;
        }
    }

    public void CheckForCrafting()
    {
        if(item1 != -1 && item2 != -1)
        {
            bool crafted = false;
            foreach (CombinationInfo info in combinations)
                if (item1 == info.items.x && item2 == info.items.y || item2 == info.items.x && item1 == info.items.y)
                {
                    craftedItem = info.combination;
                    break;
                }
            item1 = -1;
            item2 = -1;
            //Destroy(Instantiate(crafted ? accepted : rejected, transform.position, Quaternion.identity), particleLifetime);
        }
    }
}

[System.Serializable]
public class CombinationInfo
{
    public Vector2Int items;
    public int combination;
}
