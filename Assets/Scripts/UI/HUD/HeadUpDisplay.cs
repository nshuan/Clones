using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HeadUpDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text soulText;
        [SerializeField] private TMP_Text coinText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Slider healthBar;

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                scoreText.text = value.ToString();
            }
        }

        private float _timePlayed;
        public float TimePlayed
        {
            get => _timePlayed;
            set
            {
                _timePlayed = value;
                timerText.text = value.ToString("0.00");
            }
        }

        private int _soul;
        public int Soul
        {
            get => _soul;
            set
            {
                _soul = value;
                soulText.text = value.ToString();
            }
        }

        private int _coin;
        public int Coin
        {
            get => _coin;
            set
            {
                _coin = value;
                coinText.text = value.ToString();
            }
        }

        private int _level;
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                levelText.text = value.ToString();
            }
        }

        private void Awake()
        {
            healthBar.maxValue = 1f;
            healthBar.minValue = 0f;
            healthBar.value = 1f;
        }

        public void UpdateHealthBar(float range)
        {
            healthBar.value = range * healthBar.maxValue;
        }

    }
}