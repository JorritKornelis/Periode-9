using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    [Header("GenaralStuff")]
    public GameObject startPanel;
    public GameObject optionsPanel;
    public string startSceneToLoad;

    private void Awake()
    {
        optionsPanel.SetActive(false);
    }

    //genarl stuff
    public void StartButtonFunction()
    {
        SceneManager.LoadScene(startSceneToLoad);
    }

    public void SwitchOptionsButtonFunction()
    {
        if (optionsPanel.activeSelf == false)
        {
            startPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }
        else
        {
            startPanel.SetActive(true);
            optionsPanel.SetActive(false);
        }
    }

    public void QuitButtonFunction()
    {
        Debug.Log("Player has Quit");
        Application.Quit();
    }
}
