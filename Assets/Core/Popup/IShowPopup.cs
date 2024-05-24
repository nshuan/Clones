using UnityEngine;

namespace Core.Popup
{
    public interface IPopupPresenter
    {
        void OnShowPopup();
        void OnShowPopup(Transform parent);
    }
}