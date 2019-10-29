using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShop : MonoBehaviour
{
    public string savingTag;
    public Saving saving;
    public GameObject shop1, shop2;

    public void Start()
    {
        saving = GameObject.FindWithTag(savingTag).GetComponent<Saving>();
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        shop1.SetActive((saving.data.unlocks.isUpgradeShop == 0) ? true : false);
        shop2.SetActive((saving.data.unlocks.isUpgradeShop == 1) ? true : false);
    }
}
