using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActivator : MonoBehaviour
{

    public KeyCode leInput;
    public Transform location;
    public GameObject prefab;
    public bool worldSpace;

    public bool destroyAfterTime;
    public float destroyTimer;

    public bool outsideInput;
    [HideInInspector]
    public bool outsideCanBeUsed;

    private void Awake()
    {
        if (!outsideInput)
        {
            outsideCanBeUsed = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(leInput) && outsideCanBeUsed)
        {
            GameObject g = null;

            g = Instantiate(prefab, location, worldSpace);

            if (destroyAfterTime)
            {
                Destroy(g, destroyTimer);
            }
        }
    }
}
