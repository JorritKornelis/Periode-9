using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuInGame : MonoBehaviour
{
    public GameObject optionsPanel;
    bool open = false;

    void Awake()
    {
        optionsPanel.SetActive(false);
    }

    void Update()
    {
        OpenOptionsInGameFunction();
    }

    public void OpenOptionsInGameFunction()
    {
        open = true;
        if (Input.GetButton("Escape") && open == true)
        {
            optionsPanel.SetActive(!optionsPanel.activeSelf);
            open = false;
        }
    }

}
