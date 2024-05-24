using System;
using Managers;
using Scripts.PlayerSettings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.Home.SelectCharacter
{
    public class CharacterPreview : MonoBehaviour
    {
        [SerializeField] private SelectCharacterButton selectButton;
        [SerializeField] private UnlockCharacterButton unlockButton;
        
        [SerializeField] private Image bigImage;

        private PlayerCharacterSO _characterData;

        private void OnEnable()
        {
            PlayerManager.OnChangeCharacter += OnChangeCharacter;
            PlayerManager.OnUnlockCharacter += OnUnlockCharacter;
        }

        private void OnDisable()
        {
            PlayerManager.OnChangeCharacter -= OnChangeCharacter;
            PlayerManager.OnUnlockCharacter -= OnUnlockCharacter;
        }

        public void Setup(PlayerCharacterSO characterData)
        {
            _characterData = characterData;
            
            bigImage.sprite = _characterData.AvatarSprite;
            bigImage.color = _characterData.Color;
            
            // Setup buttons
            var unlocked = PlayerManager.Instance.GetCharacterStatus(_characterData.Id);
            selectButton.gameObject.SetActive(unlocked);
            selectButton.Setup(characterData);
            unlockButton.gameObject.SetActive(!unlocked);
            unlockButton.Setup(characterData);
        }

        private void OnChangeCharacter(int id)
        {

        }

        private void OnUnlockCharacter(int id)
        {
            selectButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);
        }
    }
}