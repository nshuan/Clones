using Core.Popup;
using TMPro;
using UnityEngine;

namespace UI.InGame
{
    public class GameOverPopup : MonoBehaviour, IPopup
    {
        [SerializeField] private TMP_Text finalScore;
        [SerializeField] private TMP_Text finalTime;
        [SerializeField] private TMP_Text finalCoin;
        [SerializeField] private TMP_Text finalSoul;
        
        public void SetupResult(string scoreText, string timerText, string coinText, string soulText)
        {
            finalScore.text = scoreText;
            finalTime.text = timerText;
            finalCoin.text = coinText;
            finalSoul.text = soulText;
        }
    }
}