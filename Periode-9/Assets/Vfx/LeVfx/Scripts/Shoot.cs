using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject prefab;
    public string name;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject g = Instantiate(prefab, transform);
            g.transform.forward = Camera.main.transform.forward;
        }

        if (Input.GetMouseButton(1))
        {
            GameObject g = Instantiate(prefab, transform);
            g.transform.forward = Camera.main.transform.forward;
        }
    }
}
