using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Home.Settings
{
    public class ClearChangesButton : MonoBehaviour, IPointerClickHandler
    {
        public Action ClearAction { get; set; }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ClearAction?.Invoke();
        }
    }
}