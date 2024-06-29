using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Home.Settings
{
    public class SettingSoundVolume : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TMP_Text volumeText;
    
        private void OnEnable()
        {
            volumeSlider.minValue = 0;
            volumeSlider.maxValue = 100;
            volumeSlider.value = SoundManager.Instance.SoundVolume;
            volumeText.text = SoundManager.Instance.SoundVolume.ToString();

            volumeSlider.onValueChanged.AddListener((val) =>
            {
                volumeText.text = ((int)val).ToString();
                SoundManager.Instance.SoundVolume = (int)val;
            });
            SettingsPopup.OnSaveSettings += OnSaveSetting;
            SettingsPopup.OnClearChanges += OnClearChange;
        }

        private void OnDisable()
        {
            SoundManager.Instance.SoundVolume = SoundManager.Instance.SoundVolume; 
            volumeSlider.onValueChanged.RemoveAllListeners();
            SettingsPopup.OnSaveSettings -= OnSaveSetting;
            SettingsPopup.OnClearChanges -= OnClearChange;
        }

        private void OnSaveSetting()
        {
            SoundManager.Instance.SaveSoundVolume();
        }
        
        private void OnClearChange()
        {
            volumeSlider.value = SoundManager.Instance.SoundVolume;
        }
    }
}