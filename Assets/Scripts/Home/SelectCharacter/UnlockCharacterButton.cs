using Managers;
using Scripts.PlayerSettings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Home.SelectCharacter
{
    public class UnlockCharacterButton : CharacterPreviewButton
    {
        [SerializeField] private TMP_Text priceText;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (Equals(CharacterData, null)) return;
            if (!CoinManager.IsSpendable(CharacterData.UnlockPrice)) return;
            PlayerManager.Instance.UnlockCharacter(CharacterData);
        }

        public override void Setup(PlayerCharacterSO characterData)
        {
            base.Setup(characterData);
            
            priceText.text = CharacterData.UnlockPrice.ToString();
        }
    }
}