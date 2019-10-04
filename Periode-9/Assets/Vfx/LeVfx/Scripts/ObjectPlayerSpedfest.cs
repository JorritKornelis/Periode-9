using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ObjectPlayerSpedfest : MonoBehaviour
{
    public GameObject[] prefabs;

    public int maxAmount;

    public int minX;
    public int maxX;

    public int minZ;
    public int maxZ;

    public bool generate;

    private void Update()
    {
        if (generate)
        {
            generate = false;
            Place();
        }
    }

    public void Place()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            for (int i2 = 0; i2 < Random.Range(maxAmount/3, maxAmount); i2++)
            {
                Vector3 place = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
                Vector3 rotationFixer = new Vector3(-90, 0, 0);
                rotationFixer.z = Random.Range(-360, 360);
                GameObject maked = Instantiate(prefabs[i], place, Quaternion.identity);
                maked.transform.Rotate(rotationFixer);
                float newScale = Random.Range(0.7f, 1.3f);
                maked.transform.localScale = new Vector3(newScale, newScale, newScale);
                maked.name = i.ToString() + "-" + i2.ToString();
                maked.transform.SetParent(gameObject.transform);
            }
        }
    }
}
