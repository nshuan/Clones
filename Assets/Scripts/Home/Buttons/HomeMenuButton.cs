using Core.Popup;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Home.Buttons
{
    public class HomeMenuButton : PopupPresenter, IPointerClickHandler
    {
        [SerializeField] private Transform boardHolder;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnShowPopup(boardHolder);
        }
    }
}