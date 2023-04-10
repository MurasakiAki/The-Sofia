using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerSnapshot snapshot1;
    public Slider musicSlider;
    //private float sliderVolume = 0; 

    private void Start()
    {
        //musicSlider.value = sliderVolume;
        snapshot1.TransitionTo(musicSlider.value);
    }

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(MusicVolume);
        //sliderVolume = musicSlider.value;
    }

    private void MusicVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }


}
