using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 

public class OptionsMenu : MonoBehaviour {

    public AudioMixer audioMixerMaster;

    public Dropdown resolutionDropdown; 

    Resolution[] resolutions; 

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0; 
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option); 

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i; 
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }
    //volume start
    public void SetVolume (float Mastervolume)
    {
        audioMixerMaster.SetFloat("Volume", Mastervolume);     
    }

    public void SetMusicVolume(float Musicvolume)
    {
        audioMixerMaster.SetFloat("Music", Musicvolume);
    }

    public void SetSFXVolume(float SFXvolume)
    {
        audioMixerMaster.SetFloat("SFX", SFXvolume);
    }

    public void SetAmbienceVolume(float Ambiencevolume)
    {
        audioMixerMaster.SetFloat("Ambience", Ambiencevolume);
    }

    public void SetDialogueVolume(float Dialoguevolume)
    {
        audioMixerMaster.SetFloat("Dialogue", Dialoguevolume);
    }
    //volume end

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); 
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; 
    }

}
