using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Scripts.Home
{
    public class CoinPreview : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinText;
        
        private void OnEnable()
        {
            SetupText(CoinManager.CurrentCoin);
            CoinManager.OnCoinChange += OnUpdateCoin;
        }

        private void OnDisable()
        {
            CoinManager.OnCoinChange -= OnUpdateCoin;
        }

        private void OnUpdateCoin(int valueChanged)
        {
            SetupText(CoinManager.CurrentCoin);
        }

        private void SetupText(int value)
        {
            coinText.text = value.ToString();
        }
    }
}

