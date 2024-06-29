using System;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public class SurviveDurationCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        
        private float _timeCounter = 0f;

        private void Update()
        {
            // _timeCounter += Time.deltaTime * GameManager.Instance.TimeScale;
        }
        
        public void UpdateTimer(float value)
        {
            timerText.text = value.ToString("0.00");
        }
    }
}