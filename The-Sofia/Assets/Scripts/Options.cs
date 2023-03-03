using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{

    public AudioMixer audioMixer; 
    public void FullScreen(bool full)
    {
        Screen.fullScreen = full;
    }

    public void Volume(float volume)
    {
        audioMixer.SetFloat("volume", volume); 
    }
}
