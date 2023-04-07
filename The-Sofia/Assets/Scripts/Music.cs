using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerSnapshot snapshot1;

    private void Start()
    {
        snapshot1.TransitionTo(0f);
    }
}
