using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : ShopAcessScript
{
    public CombinationInfo[] combinations;
    public int item1, item2;
    public int craftedItem;
    public bool check;
    public GameObject accepted, rejected;
    public float particleLifetime;
    public CameraFocus focus;
    public int focusIndex;

    public void Update()
    {
        if (check)
        {
            CheckForCrafting();
            check = false;
        }
    }

    public override void Interact()
    {
        focus.StartCoroutine(focus.MoveTowardsPoint(focusIndex));
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
                    crafted = true;
                    break;
                }
            item1 = -1;
            item2 = -1;
            Destroy(Instantiate(crafted ? accepted : rejected, transform.position, Quaternion.identity), particleLifetime);
        }
    }
}

[System.Serializable]
public class CombinationInfo
{
    public Vector2Int items;
    public int combination;
}
