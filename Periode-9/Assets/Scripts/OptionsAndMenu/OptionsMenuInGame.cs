using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuInGame : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject settingPanel;
    bool b = false;
    public string backStartUpScene;

    void Awake()
    {
        optionsPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (settingPanel.activeSelf == false)
            {
                b = true;
                OpenOptionsInGameFunction();
            }
            else
            {
                settingPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void OpenOptionsInGameFunction()
    {
        if (b == true)
        {
            b = false;
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

    public void BackToOptions()
    {
        settingPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(backStartUpScene);
    }

    public void ContinueGame()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
