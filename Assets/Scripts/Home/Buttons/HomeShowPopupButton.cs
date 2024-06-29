using Core.Popup;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Home.Buttons
{
    public class HomeShowPopupButton : HomeMenuButton
    {
        [SerializeField] private Transform boardHolder;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            OnShowPopup(boardHolder);
            PopupCache.gameObject.SetActive(false);
            PopupCache.transform.localScale = Vector3.one;
            base.OnPointerClick(eventData);
        }

        public override Tween DoClickEffect()
        {
            PopupCache.transform.localScale = new Vector3(0f, 1f, 1f);
            PopupCache.gameObject.SetActive(true);
            return PopupCache.transform.DOScaleX(1f, 0.2f);
        }

        public override Tween DoHideEffect()
        {
            return DOTween.Sequence()
                .Append(PopupCache.transform.DOScaleX(0f, 0.2f))
                .AppendCallback(() =>
                {
                    PopupCache.gameObject.SetActive(false);
                    PopupCache.transform.localScale = Vector3.one;
                });
        }
    }
}