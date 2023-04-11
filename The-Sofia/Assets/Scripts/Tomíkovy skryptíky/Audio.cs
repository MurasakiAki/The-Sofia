using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public static Audio instance;

    public AudioMixer audioMixer; 
    public const string SAV_VOL = "musicVolume"; 
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    private void LoadVolume()
    {
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat(SAV_VOL, 0f));
    }
}
