using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputSceneSwitch : MonoBehaviour
{
    public string input, sceneSwitch;

    public void Update()
    {
        if (Input.GetButtonDown(input))
            SceneManager.LoadScene(sceneSwitch);
    }
}
