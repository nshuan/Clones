using System;
using Core.DataHandle;

namespace Managers
{
    public static class CoinManager
    {
        private const string CoinKey = "CurrentCoinKey";
        
        public static int CurrentCoin
        {
            get => DataHandler.Load<int>(CoinKey);
            set
            {
                var lastCoin = DataHandler.Load<int>(CoinKey);
                DataHandler.Save<int>(CoinKey, value);
                OnCoinChange?.Invoke(value - lastCoin);
            }
        }

        public static event Action<int> OnCoinChange; 

        public static bool IsSpendable(int amount)
        {
            return amount <= CurrentCoin;
        }
    }
}