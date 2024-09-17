using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerScript : MonoBehaviour
{
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioSource; // Zmieniona nazwa zmiennej
    public bool alreadyPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        if (!alreadyPlayed)
        {
            audioSource.PlayOneShot(SoundToPlay, Volume);
            alreadyPlayed = true;
        }
    }
    
    
}
