using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Home.SelectCharacter
{
    public class ClosePopupButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject popup;
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (popup == null) return;
            popup.SetActive(false);
        }
    }
}