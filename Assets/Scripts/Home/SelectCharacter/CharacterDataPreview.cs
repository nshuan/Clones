using Managers;
using Scripts.PlayerSettings;
using TMPro;
using UnityEngine;

namespace Scripts.Home.SelectCharacter
{
    public class CharacterDataPreview : MonoBehaviour
    {
        [SerializeField] private TMP_Text maxHealthText;
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private TMP_Text gunText;
        
        public void Setup(PlayerCharacterSO data)
        {
            maxHealthText.text = data.MaxHealth.ToMaxHealthString();
            damageText.text = data.Damage.ToDamageString();
            speedText.text = data.Speed.ToSpeedString();
            gunText.text = data.DefaultGun.ToGunTypeString();
        }

        public void HideData()
        {
            maxHealthText.gameObject.SetActive(false);
            damageText.gameObject.SetActive(false);
            speedText.gameObject.SetActive(false);
            gunText.gameObject.SetActive(false);
        }

        public void ShowData()
        {
            maxHealthText.gameObject.SetActive(true);
            damageText.gameObject.SetActive(true);
            speedText.gameObject.SetActive(true);
            gunText.gameObject.SetActive(true);
        }
    }
    
    public static class FormatPlayerDataToPreviewString
    {
        public static string ToMaxHealthString(this int maxHealth)
        {
            // return "Max health: " + maxHealth.ToString();
            return maxHealth.ToString();
        }

        public static string ToDamageString(this int damage)
        {
            // return "Damage: " + damage.ToString();
            return damage.ToString();
        }

        public static string ToSpeedString(this float speed)
        {
            // return "Speed: " + speed;
            return speed.ToString("N0");
        }

        public static string ToGunTypeString(this GunType gunType)
        {
            // return "Gun type: " + GunManager.Instance.GetGun(gunType).GetName();
            return GunManager.Instance.GetGun(gunType).GetName();
        }
    }
}