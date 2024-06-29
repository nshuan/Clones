using System;
using Core.Popup;
using UI.InGame;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class PauseButton : PopupPresenter<PausePopup>
    {
        [SerializeField] private Button button;

        protected override void Awake()
        {
            base.Awake();
            button.onClick.AddListener(PauseGame);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        private void PauseGame()
        {
            GameManager.Instance.PauseGame();
            OnShowPopup();
        }
    }
}