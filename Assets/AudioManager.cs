using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip shootSound;
    public AudioClip enemyDieSound;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.volume = 0.1f;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
<<<<<<< HEAD
        sfxSource.volume = 0.1f;
=======
        musicSource.volume = 0.1f;
>>>>>>> 2ae9621f7eeabb1e6ff7ae9807d69b6b4891489c
        sfxSource.PlayOneShot(clip);
    }
}
