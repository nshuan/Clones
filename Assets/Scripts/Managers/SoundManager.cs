using System.Collections;
using System.Collections.Generic;
using Core;
using Core.DataHandle;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoSingleton<SoundManager>
{
    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolume";

    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource soundAudioSource;
    
    [SerializeField] private AudioClip outGameBg;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip theme;
    [SerializeField] private AudioClip bossTheme;

    public int MusicVolume
    {
        get => DataHandler.Load<int>(MusicVolumeKey, 80);
        set => bgAudioSource.volume = (float)value / 100;
    }

    public int SoundVolume
    {
        get => DataHandler.Load<int>(SoundVolumeKey, 80);
        set => soundAudioSource.volume = (float)value / 100;
    }

    private float ScaledVolume => (float)MusicVolume / 100;

    public void SaveMusicVolume()
    {
        DataHandler.Save<int>(MusicVolumeKey, (int)(bgAudioSource.volume * 100));
    }

    public void SaveSoundVolume()
    {
        DataHandler.Save<int>(SoundVolumeKey, (int)(soundAudioSource.volume * 100));
    }
    
    public void PlayFireSound()
    {
        soundAudioSource.PlayOneShot(fireSound, SoundVolume);
    }

    public void PlayTheme()
    {
        bgAudioSource.Stop();
        bgAudioSource.PlayOneShot(theme, ScaledVolume);
    }

    public void PlayBossTheme()
    {
        bgAudioSource.Stop();
        bgAudioSource.PlayOneShot(bossTheme, ScaledVolume);
    }

    public void PlayMusic(AudioClip clip)
    {
        bgAudioSource.Stop();
        bgAudioSource.PlayOneShot(clip, ScaledVolume);
    }

    public void PlaySound(AudioClip clip)
    {
        soundAudioSource.PlayOneShot(clip, SoundVolume);
    }
}
