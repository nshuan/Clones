using System;
using Managers;
using Scripts.Home.Buttons;
using Scripts.PlayerSettings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.Home.SelectCharacter
{
    public class CharacterPreview : HomeMenuPopup
    {
        [SerializeField] private SelectCharacterButton selectButton;
        [SerializeField] private UnlockCharacterButton unlockButton;
        
        [SerializeField] private Image bigImage;
        [SerializeField] private CharacterDataPreview dataPreview;

        private PlayerCharacterSO _characterData;

        private void OnEnable()
        {
            InitPreview();
            
            PlayerManager.OnChangeCharacter += OnChangeCharacter;
            PlayerManager.OnUnlockCharacter += OnUnlockCharacter;
        }

        private void OnDisable()
        {
            PlayerManager.OnChangeCharacter -= OnChangeCharacter;
            PlayerManager.OnUnlockCharacter -= OnUnlockCharacter;
        }

        private void InitPreview()
        {
            bigImage.gameObject.SetActive(false);    
            dataPreview.HideData();
            selectButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(false);
        }
        
        public void Setup(PlayerCharacterSO characterData)
        {
            _characterData = characterData;
            
            bigImage.gameObject.SetActive(true);    
            dataPreview.ShowData();
            
            bigImage.sprite = _characterData.AvatarSprite;
            bigImage.color = _characterData.Color;
            
            SetupButton();
            SetupInfos();
        }

        private void OnChangeCharacter(int id)
        {

        }

        private void OnUnlockCharacter(int id)
        {
            selectButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);
        }

        private void SetupButton()
        {
            var unlocked = PlayerManager.Instance.GetCharacterStatus(_characterData.Id);
            selectButton.gameObject.SetActive(unlocked);
            selectButton.Setup(_characterData);
            unlockButton.gameObject.SetActive(!unlocked);
            unlockButton.Setup(_characterData);
        }

        private void SetupInfos()
        {
            dataPreview.Setup(_characterData);
        }
    }
}