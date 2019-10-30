using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingTable : ShopAcessScript
{
    public int cameraLoc;
    public SellPoint[] sellpoints;
    public bool currentlyInteracted;
    public SellingUI ui;
    public CameraFocus focus;
    public string playerTag;

    public override void Interact()
    {
        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        if (active)
        {
            active = false;
            inv.inv.SetActive(false);
        }
        else
        {
            StartCoroutine(focus.MoveTowardsPoint(cameraLoc));
            inv.inv.SetActive(true);
            active = true;
            InteractionStart();
        }
    }

    public void Update()
    {
        if (currentlyInteracted && Input.GetButtonDown("Inventory"))
        {
            currentlyInteracted = false;
            foreach (SellPoint point in sellpoints)
            {
                point.currentlyPlacable = false;
                point.lookedAt = false;
            }
            ui.ui.SetActive(false);
        }
    }

    public void InteractionStart()
    {
        foreach (SellPoint point in sellpoints)
        {
            point.currentlyPlacable = true;
            point.lookedAt = true;
            point.ui = ui;
        }
        currentlyInteracted = true;
    }
}
