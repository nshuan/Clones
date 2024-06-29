using System;
using Scripts.Home.Buttons;
using UnityEngine;
using UnityEngine.Serialization;

namespace Home.Settings
{
    public class SettingsPopup : HomeMenuPopup
    {
        [SerializeField] private SaveSettingsButton saveButton;
        [SerializeField] private ClearChangesButton clearButton;
        
        public static event Action OnSaveSettings;
        public static event Action OnClearChanges;

        private void OnEnable()
        {
            saveButton.SaveAction = SaveSettings;
            clearButton.ClearAction = ClearChanges;
        }

        private void SaveSettings()
        {
            OnSaveSettings?.Invoke();
        }

        private void ClearChanges()
        {
            OnClearChanges?.Invoke();
        }
    }
}