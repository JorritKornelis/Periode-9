using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
 
public class MaskLocationer : MonoBehaviour
{
    public MeshRenderer mR;
    public Material[] m;

    private void Update()
    {
        if (m != null)
        {
            for (int i = 0; i < m.Length; i++)
            {
                m[i].SetVector("_Location", transform.position);
            }
        }
    }
}
