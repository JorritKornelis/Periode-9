using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingTable : MonoBehaviour
{
    public int cameraLoc;
    public SellPoint[] sellpoints;
    public bool currentlyInteracted;
    public SellingUI ui;

    public void Update()
    {
        if (currentlyInteracted && Input.GetButtonDown("Inventory"))
        {
            currentlyInteracted = false;
            foreach (SellPoint point in sellpoints)
                point.currentlyPlacable = false;
        }
    }

    public void InteractionStart()
    {
        foreach (SellPoint point in sellpoints)
        {
            point.currentlyPlacable = true;
            point.ui = ui;
        }
        currentlyInteracted = true;
    }
}
