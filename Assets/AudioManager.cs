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
    public AudioClip lastBossMusic;

    private void Start()
    {
        // Cari EnemySpawner untuk menentukan musik awal sesuai wave
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            PlayBGM(spawner.currentWaveCount);  // Pastikan lagu sesuai wave saat game dimulai
        }
        else
        {
            // Fallback jika spawner tidak ditemukan
            musicSource.clip = backgroundMusic;
            musicSource.volume = 0.1f;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // Fungsi untuk memutar BGM sesuai wave
    public void PlayBGM(int currentWaveCount)
    {
        if (currentWaveCount + 1 >= 10)  // Jika mencapai stage 10 atau lebih
        {
            lastBossMusicPlay();
        }
        else
        {
            if (musicSource.clip != backgroundMusic)
            {
                musicSource.clip = backgroundMusic;
                musicSource.volume = 0.1f;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }

    // Fungsi untuk memutar lagu boss terakhir
    public void lastBossMusicPlay()
    {
        musicSource.clip = lastBossMusic;
        musicSource.volume = 0.1f;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Fungsi untuk memutar SFX
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.volume = 0.1f;
        sfxSource.PlayOneShot(clip);
    }
}
