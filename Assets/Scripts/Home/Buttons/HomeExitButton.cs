using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Home.Buttons
{
    public class HomeExitButton : HomeMenuButton
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            Navigator.ExitGame();
        }

        public override void OnShowPopup(Transform parent)
        {
            
        }
    }
}