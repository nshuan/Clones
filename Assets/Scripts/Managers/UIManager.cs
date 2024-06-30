using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.HUD;
using UI.InGame;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [Header("Player data")]
    [SerializeField] private Transform inforBoard;

    [SerializeField] private HeadUpDisplay hud;

    #region Boss Informations
    [SerializeField] private Transform bossInforBoard;
    [SerializeField] private TMP_Text bossNameText;
    [SerializeField] private Slider bossHealthBar;
    #endregion

    #region Field parents
    [Header("Dynamic text")]
    [SerializeField] private Transform floatTextParent;
    #endregion

    #region Prefabs
    [SerializeField] private GameObject floatTextPref;
    #endregion

    #region Choice filter
    public ChooseItem chooseItemBoard;
    [HideInInspector] public bool chosingItem = false;
    public ChooseGunSlot replaceGunBoard;
    [HideInInspector] public bool replacingGun = false;
    #endregion
    
    #region Temporary variables
    private bool timerOn = true;
    private float timeCounter = 0f;

    private bool bossIntro = false;
    private float bossIntroTime = 0f;
    private float bossIntroCounter = 0f;
    #endregion

    #region Gameover
    [SerializeField] private GameOverPresenter gameOverPresenter;
    #endregion

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!timerOn) return;

        // Timer of player's survived time
        timeCounter += Time.deltaTime * GameManager.Instance.TimeScale;
        UpdateTimer(timeCounter);

        // Boss introduction
        if (!bossIntro) return;
        if (bossIntroCounter < bossIntroTime)
        {
            bossIntroCounter += Time.deltaTime;
        }
        else
        {
            bossIntro = false;
            bossIntroCounter = 0f;
            GameManager.Instance.BossFightStart();
        }
    }

    public void GameOver()
    {
        inforBoard.gameObject.SetActive(false);
        gameOverPresenter.SetupGameOver(
            hud.Score.ToString(), 
            hud.TimePlayed.ToString("0.00"), 
            hud.Coin.ToString(), 
            hud.Soul.ToString());
    }

    #region Informations
    public void UpdateScore(int value)
    {
        hud.Score = value;
    }

    public void UpdateTimer(float value)
    {
        hud.TimePlayed = value;
    }

    public float StopTimer()
    {
        timerOn = false;

        return timeCounter;
    }

    public void UpdateSoul(int value)
    {
        hud.Soul = value;
    }

    public void UpdateCoin(int value)
    {
        hud.Coin = value;
    }

    public void UpdateLevel(int value)
    {
        hud.Level = value;
    }

    public void UpdateHealthBar(int remainHealth, int maxHealth)
    {
        remainHealth = Mathf.Max(remainHealth, 0);
        hud.UpdateHealthBar((float) remainHealth / maxHealth);
    }
    #endregion

    #region Dynamic
    public void CreateFloatText(Transform target, int value, Color color)
    {
        GameObject newFloatText = Instantiate(floatTextPref, floatTextParent);
        newFloatText.GetComponent<FloatText>().Setup(target, 0.5f, value, color);
    }

    public void ChooseItem(int number, ref List<ItemCrystal> items)
    {
        chosingItem = true;

        chooseItemBoard.SetupCards(number, ref items);
        chooseItemBoard.gameObject.SetActive(true);
    }

    public void ReplaceGun(int newGunId)
    {
        replacingGun = true;

        // replaceGunBoard.SetupCards(ref GameManager.Instance.playerScript.inventory, newGunId);
        replaceGunBoard.gameObject.SetActive(true);
    }
    #endregion

    #region Boss Informations
    public void BossFightIntro(float duration)
    {
        bossIntro = true;
        bossIntroTime = duration;
    }

    public void BossFightUI(string bossName)
    {
        bossNameText.text = bossName;
        bossHealthBar.maxValue = 1f;
        bossHealthBar.minValue = 0f;
        bossHealthBar.value = 1f;

        bossInforBoard.gameObject.SetActive(true);
    }

    public void BossFightUIOff()
    {
        bossInforBoard.gameObject.SetActive(false);
    }

    public void UpdateBossHealthBar(int remainHealth, int maxHealth)
    {
        remainHealth = Mathf.Max(remainHealth, 0);
        bossHealthBar.value = ((float) remainHealth / maxHealth) * bossHealthBar.maxValue;
    }
    #endregion
}
