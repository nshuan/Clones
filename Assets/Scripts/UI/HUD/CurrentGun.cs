using System;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    [DefaultExecutionOrder(999)]
    public class CurrentGun : MonoBehaviour
    {
        [SerializeField] private TMP_Text gunNameText;
        
        private void OnEnable()
        {
            gunNameText.text = GameManager.Instance.playerScript.CurrentGun.GetName();
        }
    }
}