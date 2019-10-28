using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuInGame : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject settingPanel;

    void Awake()
    {
        optionsPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    void Update()
    {
        OpenOptionsInGameFunction();
    }

    public void OpenOptionsInGameFunction()
    {
        if (Input.GetButton("Escape"))
        {
            optionsPanel.SetActive(!optionsPanel.activeSelf);
            if (optionsPanel.activeSelf == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
    }

    public void OpenSettingsInGameFunction()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
        optionsPanel.SetActive(!settingPanel.activeSelf);
    }

}
