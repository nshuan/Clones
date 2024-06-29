using Scripts.PlayerSettings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Home.SelectCharacter
{
    public class SelectCharacterButton : CharacterPreviewButton
    {
        [SerializeField] private TMP_Text selectText;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (Equals(CharacterData, null)) return;
            PlayerManager.Instance.SetCharacter(CharacterData.Id);
            selectText.text = "SELECTED";
        }

        public override void Setup(PlayerCharacterSO characterData)
        {
            base.Setup(characterData);
            
            selectText.text = PlayerManager.Instance.CurrentCharacter == CharacterData ? "SELECTED" : "SELECT";
            var butColor = buttonImage.color;
            butColor.a = PlayerManager.Instance.CurrentCharacter == CharacterData ? 0 : 0.75f;
            buttonImage.color = butColor;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (PlayerManager.Instance.CurrentCharacter == CharacterData) return;
            base.OnPointerExit(eventData);
        }
    }

    public abstract class CharacterPreviewButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image buttonImage;
        
        protected PlayerCharacterSO CharacterData;
        public abstract void OnPointerClick(PointerEventData eventData);

        public virtual void Setup(PlayerCharacterSO characterData)
        {
            CharacterData = characterData;
        }
        
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            var butColor = buttonImage.color;
            butColor.a = 0;
            buttonImage.color = butColor;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            var butColor = buttonImage.color;
            butColor.a = 0.75f;
            buttonImage.color = butColor;
        }
    }
}