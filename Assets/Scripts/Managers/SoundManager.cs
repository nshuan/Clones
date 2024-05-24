using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource theme;
    [SerializeField] private AudioSource bossTheme;

    void Awake()
    {
        Instance = this;

        theme.Play();
    }

    public void PlayFireSound()
    {
        fireSound.Play();
    }

    public void PlayTheme()
    {
        bossTheme.Stop();
        theme.UnPause();
    }

    public void PlayBossTheme()
    {
        theme.Pause();
        bossTheme.Play();
    }
}
