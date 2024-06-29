using System;
using Core.DataHandle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Home.Settings
{
    public class SettingMusicVolume : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TMP_Text volumeText;
        
        private void OnEnable()
        {
            volumeSlider.minValue = 0;
            volumeSlider.maxValue = 100;
            volumeSlider.value = SoundManager.Instance.MusicVolume;
            volumeText.text = SoundManager.Instance.MusicVolume.ToString();

            volumeSlider.onValueChanged.AddListener((val) =>
            {
                volumeText.text = ((int)val).ToString();
                SoundManager.Instance.MusicVolume = (int)val;
            });
            SettingsPopup.OnSaveSettings += OnSaveSetting;
            SettingsPopup.OnClearChanges += OnClearChange;
        }

        private void OnDisable()
        {
            SoundManager.Instance.MusicVolume = SoundManager.Instance.MusicVolume;
            volumeSlider.onValueChanged.RemoveAllListeners();
            SettingsPopup.OnSaveSettings -= OnSaveSetting;
            SettingsPopup.OnClearChanges -= OnClearChange;
        }

        private void OnSaveSetting()
        {
            SoundManager.Instance.SaveMusicVolume();
        }
        
        private void OnClearChange()
        {
            volumeSlider.value = SoundManager.Instance.MusicVolume;
        }
    }
}