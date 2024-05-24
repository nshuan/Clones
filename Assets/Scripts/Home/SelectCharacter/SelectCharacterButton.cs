using Scripts.PlayerSettings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
        }
    }

    public abstract class CharacterPreviewButton : MonoBehaviour, IPointerClickHandler
    {
        protected PlayerCharacterSO CharacterData;
        public abstract void OnPointerClick(PointerEventData eventData);

        public virtual void Setup(PlayerCharacterSO characterData)
        {
            CharacterData = characterData;
        }
    }
}