using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class ContinueButton : MonoBehaviour
    {
        [SerializeField] private GameObject pausePopup;
        [SerializeField] private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(OnContinueGame);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        private void OnContinueGame()
        {
            pausePopup.SetActive(false);
            GameManager.Instance.ResumeGame();
        }
    }
}