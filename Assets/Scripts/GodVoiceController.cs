using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodVoiceController : MonoBehaviour
{
    public AudioClip[] DeathSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathSound()
    {
        if (DeathSounds.Length != 0)
        {
            audioSource.clip = DeathSounds[Random.Range(0, DeathSounds.Length - 1)];
            audioSource.Play();
        }
    }
}
