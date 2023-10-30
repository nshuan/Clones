using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    [SerializeField] private AudioSource fireSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlayFireSound()
    {
        fireSound.Play();
    }
}
