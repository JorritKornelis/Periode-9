using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsSettings : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider masterSlider, npcSlider, vfxSlider, uISlider, musicSlider;

    [Header("Res")]
    public Dropdown dropDownRes;
    Resolution[] resolutions;

    //ooption in start
    void Start()
    {
        //debug quality v
        //int qualityLevel = QualitySettings.GetQualityLevel();
        //Debug.Log(qualityLevel);

        /*masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeMix", 0);
        npcSlider.value = PlayerPrefs.GetFloat("MasterVolumeMix", 0);
        vfxSlider.value = PlayerPrefs.GetFloat("MasterVolumeMix", 0);
        uISlider.value = PlayerPrefs.GetFloat("MasterVolumeMix", 0);
        musicSlider.value = PlayerPrefs.GetFloat("MasterVolumeMix", 0);*/

        resolutions = Screen.resolutions;

        dropDownRes.ClearOptions();
        List<string> vs = new List<string>();
        int curIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string options = resolutions[i].width + " X " + resolutions[i].height;
            vs.Add(options);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                curIndex = i;
            }
        }
        dropDownRes.AddOptions(vs);
        dropDownRes.value = curIndex;
        dropDownRes.RefreshShownValue();
    }

    //options
    public void SwitchRes(int witch)
    {
        Resolution resol = resolutions[witch];
        Screen.SetResolution(resol.width, resol.height, Screen.fullScreen);
    }

    public void FullScreenToggle(bool tog)
    {
        Screen.fullScreen = tog;
    }

    //sliders
    public void CoppleMasterVolume(float amount)
    {
        audioMixer.SetFloat("MasterSlider", amount);
        //PlayerPrefs.SetFloat("MasterVolumeMix", amount);
    }
    public void CoppleNpcVolume(float amount)
    {
        audioMixer.SetFloat("NpcSlider", amount);
    }
    public void CoppleVfxVolume(float amount)
    {
        audioMixer.SetFloat("VfxSlider", amount);
    }
    public void CoppleUiVolume(float amount)
    {
        audioMixer.SetFloat("UiSlider", amount);
    }
    public void CoppleMusicVolume(float amount)
    {
        Debug.Log(amount);
        audioMixer.SetFloat("MusicSlider", amount);
        //PlayerPrefs.SetFloat("MasterVolumeMix", amount);
    }

    public void SetGraphics(int dropDown)
    {
        //QualitySettings.SetQualityLevel(dropDown);
        QualitySettings.SetQualityLevel(dropDown, true);
        Debug.Log(dropDown);
    }
}
