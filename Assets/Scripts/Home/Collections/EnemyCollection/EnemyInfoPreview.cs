using Characters;
using EnemyCore.EnemyData;
using Managers;
using Scripts.PlayerSettings;
using TMPro;
using UnityEngine;

namespace Scripts.Home.Collections
{
    public class EnemyInfoPreview : MonoBehaviour
    {
        [SerializeField] private TMP_Text maxHealthText;
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private TMP_Text gunText;
        [SerializeField] private TMP_Text enemyName;
        
        public void Setup(EnemyStats data)
        {
            maxHealthText.text = data.MaxHealth.ToMaxHealthString();
            damageText.text = data.BaseDamage.ToDamageString();
            speedText.text = data.Speed.ToSpeedString();
            gunText.text = data.GunType.ToGunTypeString();
            enemyName.text = data.Name;
            enemyName.color = data.VisualColor;
        }
        
        public void HideData()
        {
            maxHealthText.gameObject.SetActive(false);
            damageText.gameObject.SetActive(false);
            speedText.gameObject.SetActive(false);
            gunText.gameObject.SetActive(false);
            enemyName.gameObject.SetActive(false);
        }

        public void ShowData()
        {
            maxHealthText.gameObject.SetActive(true);
            damageText.gameObject.SetActive(true);
            speedText.gameObject.SetActive(true);
            gunText.gameObject.SetActive(true);
            enemyName.gameObject.SetActive(true);
        }
    }
    
    public static class FormatEnemyDataToPreviewString
    {
        public static string ToEnemyTypeString(this EnemyType enemyType)
        {
            // return "Type: " + enemyType.ToString();
            return enemyType.ToString();
        }

        public static string ToEnemyClassString(this EnemyClass enemyClass)
        {
            // return "Class: " + enemyClass.ToString();
            return enemyClass.ToString();
        }
        
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