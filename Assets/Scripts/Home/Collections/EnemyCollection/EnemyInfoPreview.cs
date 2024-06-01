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
        public void Setup(EnemyStats data)
        {
            maxHealthText.text = data.MaxHealth.ToMaxHealthString();
            damageText.text = data.BaseDamage.ToDamageString();
            speedText.text = data.Speed.ToSpeedString();
            gunText.text = data.GunType.ToGunTypeString();
        }
    }
    
    public static class FormatEnemyDataToPreviewString
    {
        public static string ToEnemyTypeString(this EnemyType enemyType)
        {
            return "Type: " + enemyType.ToString();
        }

        public static string ToEnemyClassString(this EnemyClass enemyClass)
        {
            return "Class: " + enemyClass.ToString();
        }
        
        public static string ToMaxHealthString(this int maxHealth)
        {
            return "Max health: " + maxHealth.ToString();
        }

        public static string ToDamageString(this int damage)
        {
            return "Damage: " + damage.ToString();
        }

        public static string ToSpeedString(this float speed)
        {
            return "Speed: " + speed;
        }

        public static string ToGunTypeString(this GunType gunType)
        {
            return "Gun type: " + GunManager.Instance.GetGun(gunType).GetName();
        }
    }
}