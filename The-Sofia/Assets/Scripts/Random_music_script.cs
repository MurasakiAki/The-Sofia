using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_music_script : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomAudio());
    }

    private System.Collections.IEnumerator PlayRandomAudio()
    {
        while (true)
        {
            if (audioClips.Length > 0)
            {
                int randomIndex = Random.Range(0, audioClips.Length);
                AudioClip randomClip = audioClips[randomIndex];
                audioSource.clip = randomClip;
                audioSource.Play();

                yield return new WaitForSeconds(audioSource.clip.length);
            }
            else
            {
                Debug.LogError("No audio clips assigned!");
                yield break;
            }
        }
    }
}
