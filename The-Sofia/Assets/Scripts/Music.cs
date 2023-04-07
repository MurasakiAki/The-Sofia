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
    }

  
}
