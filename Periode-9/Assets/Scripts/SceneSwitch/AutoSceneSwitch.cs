using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneSwitch : MonoBehaviour
{
    public string sceneName;

    public void Start()
    {
        SceneManager.LoadScene(sceneName);
    }
}
