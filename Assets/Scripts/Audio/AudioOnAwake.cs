using System;
using UnityEngine;

namespace Audio
{
    public class AudioOnAwake : MonoBehaviour
    {
        [SerializeField] private AudioClip music;

        private void Awake()
        {
            if (music is null) return;
            SoundManager.Instance.PlayMusic(music);
        }
    }
}