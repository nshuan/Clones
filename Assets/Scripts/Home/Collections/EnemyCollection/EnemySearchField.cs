using System;
using TMPro;
using UnityEngine;

namespace Scripts.Home.Collections
{
    public class EnemySearchField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;

        public static event Action<string> OnEnemySearch;
        
        private void OnEnable()
        {
            inputField.onValueChanged.AddListener(OnSearch);
        }

        private void OnSearch(string value)
        {
            OnEnemySearch?.Invoke(value);
        }
            
    }
}