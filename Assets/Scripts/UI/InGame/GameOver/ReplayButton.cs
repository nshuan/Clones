using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class ReplayButton : MonoBehaviour
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
            Navigator.LoadGameScene();
        }
    }
}