using System;
using UnityEngine;

namespace Core.Popup
{
    public abstract class PopupPresenter : MonoBehaviour, IPopupPresenter
    {
        [SerializeField] private GameObject popup;
        private Canvas _canvas;

        private GameObject _popupCache;

        protected virtual void Awake()
        {
            _canvas = FindObjectOfType<Canvas>();
        }

        public virtual void OnShowPopup()
        {
            if (Equals(_popupCache, null))
            {
                if (Equals(popup.activeSelf, false))
                {
                    Debug.LogWarning("Popup reference has not been set!");
                }
                var prefabStatus = popup.activeSelf;
                popup.SetActive(false);
                _popupCache = Instantiate(popup, _canvas.transform);
                popup.SetActive(prefabStatus);
            }    
            
            _popupCache.SetActive(true);
        }

        public virtual void OnShowPopup(Transform parent)
        {
            if (Equals(_popupCache, null))
            {
                var prefabStatus = popup.activeSelf;
                popup.SetActive(false);
                _popupCache = Instantiate(popup, parent);
                popup.SetActive(prefabStatus);
            }    
            
            _popupCache.SetActive(true);
        }
    }
}