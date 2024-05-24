using Core.EndlessScroll;
using Core.InfiniteListView;
using Scripts.PlayerSettings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Home.SelectCharacter
{
    public class CharacterPreviewElement : ListViewElement<CharacterPreviewInfo>, SimpleCell.ICellView, IPointerClickHandler
    {
        [SerializeField] private Image elementImage;
        private CharacterPreview _previewer;
        
        private PlayerCharacterSO _elementData;
        
        public override void Setup(CharacterPreviewInfo participantInfo, int rank)
        {
            _elementData = participantInfo.CharacterData;
            elementImage.sprite = _elementData.AvatarSprite;
            elementImage.color = _elementData.Color;
        }

        public void SetupPreviewer(CharacterPreview target)
        {
            _previewer = target;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _previewer.Setup(_elementData);
            if (_previewer.gameObject.activeSelf == false) _previewer.gameObject.SetActive(true);
        }
    }

    public class CharacterPreviewInfo : IElementInfo
    {
        public PlayerCharacterSO CharacterData { get; set; }
    }

    public class CharacterPreviewData : ElementData<CharacterPreviewInfo, CharacterPreviewElement>
    {
        public CharacterPreview Previewer { get; set; }
        
        protected override void Setup(CharacterPreviewElement cellView)
        {
            cellView.SetupPreviewer(Previewer);
            cellView.Setup(ElementInfo, ElementId);
        }
    }
}