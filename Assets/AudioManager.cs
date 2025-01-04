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
        sfxSource.volume = 0.1f;
        sfxSource.PlayOneShot(clip);
    }
}
