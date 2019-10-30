using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTilt : MonoBehaviour
{
    public GameObject obj;
    public Vector3 standardScale;
    public float scaleIncrease;
    public Vector3 scaledMoveOffset;

    public void Start()
    {
        standardScale = obj.transform.localScale;
    }

    public void OnHoverEnter()
    {
        obj.transform.localScale *= scaleIncrease;
        obj.transform.position += scaledMoveOffset;
    }

    public void OnHoverExit()
    {
        obj.transform.localScale = standardScale;
        obj.transform.position -= scaledMoveOffset;
    }
}
