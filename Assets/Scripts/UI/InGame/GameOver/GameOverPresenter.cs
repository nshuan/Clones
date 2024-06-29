using Core.Popup;

namespace UI.InGame
{
    public class GameOverPresenter : PopupPresenter<GameOverPopup>
    {
        public void SetupGameOver(string scoreText, string timerText, string coinText, string soulText)
        {
            OnShowPopup();
            PopupCache.SetupResult(scoreText, timerText, coinText, soulText);
        }
    }
}