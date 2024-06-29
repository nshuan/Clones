using System;
using UnityEngine;

namespace Core.Popup
{
    public abstract class PopupPresenter<T> : MonoBehaviour, IPopupPresenter where  T : MonoBehaviour, IPopup
    {
        [SerializeField] private T popup;
        private Canvas _canvas;

        protected T PopupCache;

        protected virtual void Awake()
        {
            _canvas = FindObjectOfType<Canvas>();
        }

        public virtual void OnShowPopup()
        {
            if (Equals(PopupCache, null))
            {
                if (Equals(popup.gameObject.activeSelf, false))
                {
                    Debug.LogWarning("Popup reference has not been set!");
                }
                var prefabStatus = popup.gameObject.activeSelf;
                popup.gameObject.SetActive(false);
                PopupCache = Instantiate(popup, _canvas.transform);
                popup.gameObject.SetActive(prefabStatus);
            }    
            
            PopupCache.gameObject.SetActive(true);
        }

        public virtual void OnShowPopup(Transform parent)
        {
            if (Equals(PopupCache, null))
            {
                var prefabStatus = popup.gameObject.activeSelf;
                popup.gameObject.SetActive(false);
                PopupCache = Instantiate(popup, parent);
                popup.gameObject.SetActive(prefabStatus);
            }    
            
            PopupCache.gameObject.SetActive(true);
        }
    }

    public interface IPopup
    {
        
    }
}