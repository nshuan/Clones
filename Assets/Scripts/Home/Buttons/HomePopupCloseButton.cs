using System;
using Home;
using Scripts.Home.SelectCharacter;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Home.Buttons
{
    public class HomePopupCloseButton : MonoBehaviour, IPointerClickHandler
    {
        private HomeNavigator _homeNavigator;
        
        private void Awake()
        {
            _homeNavigator = GetComponentInParent<HomeNavigator>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _homeNavigator.OnClosePopup();
        }
    }
}