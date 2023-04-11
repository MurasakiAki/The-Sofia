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


    private void Start()
    {
        snapshot1.TransitionTo(musicSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat(Audio.SAV_VOL, 0f);
    }

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(MusicVolume);
    }

    private void MusicVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(Audio.SAV_VOL, musicSlider.value);
    }


}
