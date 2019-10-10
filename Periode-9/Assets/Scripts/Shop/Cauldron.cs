using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public ItemClassScriptableObject items;
    public GameObject ui;

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
        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        if (active)
        {
            active = false;
            ui.SetActive(false);
            inv.inv.SetActive(false);
        }
        else
        {
            StartCoroutine(focus.MoveTowardsPoint(focusIndex));
            inv.inv.SetActive(true);
            ui.SetActive(true);
            active = true;
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
