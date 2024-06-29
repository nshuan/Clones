using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Home.Settings
{
    public class SaveSettingsButton : MonoBehaviour, IPointerClickHandler
    {
        public Action SaveAction { get; set; }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            SaveAction?.Invoke();
        }
    }
}