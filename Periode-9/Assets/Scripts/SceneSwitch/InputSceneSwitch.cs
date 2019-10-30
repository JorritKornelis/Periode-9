using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputSceneSwitch : MonoBehaviour
{
    public string input, sceneSwitch;
    public float timer;
    public Image loadingDisplay;

    public void Update()
    {
        if (Input.GetButtonDown(input))
            StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        float time = timer;
        loadingDisplay.gameObject.SetActive(true);
        while(time >= 0)
        {
            loadingDisplay.fillAmount = (1f / timer) * (timer - time);
            time -= Time.deltaTime;
            yield return null;
            if (Input.GetButtonUp(input))
                break;
        }
        loadingDisplay.gameObject.SetActive(false);
        if (Input.GetButton(input))
            SceneManager.LoadScene(sceneSwitch);
    }
}
