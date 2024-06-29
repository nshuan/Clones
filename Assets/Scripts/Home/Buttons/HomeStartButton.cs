using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Home.Buttons
{
    public class HomeStartButton : HomeMenuButton
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            Navigator.LoadGameScene();
        }

        public override void OnShowPopup(Transform parent)
        {
            
        }
    }
}