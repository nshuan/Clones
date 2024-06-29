using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class QuitHomeButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(OnQuitHome);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        private void OnQuitHome()
        {
            Navigator.LoadMenuScene();
        }
    }
}