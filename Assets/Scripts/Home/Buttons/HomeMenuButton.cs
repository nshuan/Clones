using System;
using Core.Popup;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Home.Buttons
{
    public class HomeMenuButton : PopupPresenter<HomeMenuPopup>, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public static event Action<HomeMenuButton> OnButtonClick;

        public virtual Tween DoClickEffect()
        {
            return DOTween.Sequence();
        }

        public virtual Tween DoHideEffect()
        {
            return DOTween.Sequence();
        }

        public void HidePopup()
        {
            PopupCache.gameObject.SetActive(false);
        }
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnButtonClick?.Invoke(this);    
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, 0.1f).SetEase(Ease.InBack);
        }
    }

    public abstract class HomeMenuPopup : MonoBehaviour, IPopup
    {
        
    }
}