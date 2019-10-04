using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSelector : MonoBehaviour
{
    public GameObject[] spells;

    GameObject selectedSpell;
    int selected = 0;

    public Text text;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            selected++;
            if(selected >= spells.Length)
            {
                selected = 0;
            }
        }

        for (int i = 0; i < spells.Length; i++)
        {
            if(i == selected)
            {
                spells[i].SetActive(true);
            }
            else
            {
                spells[i].SetActive(false);
            }
        }

        text.text = spells[selected].GetComponent<Shoot>().name.ToString();
    }
}
