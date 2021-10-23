using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer mixerMusic;
    public AudioMixer mixerSFX;

    public void setMusic(float sliderValue)
    {
        mixerMusic.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void setSFX(float sliderValue)
    {
        mixerSFX.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
}
