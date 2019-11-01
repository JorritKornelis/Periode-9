using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneSwitch : MonoBehaviour
{
    public string sceneName;

    public void Start()
    {
        StartCoroutine(Delay());
    }

    public IEnumerator Delay()
    {
        yield return null;
        SceneManager.LoadScene(sceneName);
    }
}
