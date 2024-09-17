using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningSoundScript : MonoBehaviour
{
    public List<AudioClip> WalkSounds;
    public AudioSource audioSource;
    public int pos;
    public void playSound()
    {
        pos = (int)Mathf.Floor(Random.Range(0, WalkSounds.Count));
        audioSource.PlayOneShot(WalkSounds[pos]);
    }
    
}
