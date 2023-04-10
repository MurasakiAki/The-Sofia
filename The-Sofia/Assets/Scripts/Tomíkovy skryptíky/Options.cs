using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 

public class Options : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider slider;

    public void Start()
    {
        audioMixer.SetFloat("volume", slider.value);
    }


    //full scr
    public void FullScreen(bool full)
    {
        Screen.fullScreen = full;
    }

   
    
}
