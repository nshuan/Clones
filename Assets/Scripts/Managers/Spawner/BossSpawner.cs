using System;
using System.Collections;
using EnemyCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers.Spawner
{
    public class BossSpawner : MonoBehaviour
    {
        // public BossCollection bossCollection;
        [SerializeField] private GameObject theDot;
        
        public Transform CurrentBoss { get; private set; }

        public void DoSpawn(SpawnerThreshold spawnData)
        {
            SummonBoss(theDot.transform);
        }
        
        private void SummonBoss(Transform boss)
        {
            if (EnemyManager.Instance.bossing) return;

            CurrentBoss = Instantiate(boss, (Vector2) GameManager.Instance.player.transform.position + 20f * Random.insideUnitCircle.normalized, Quaternion.identity).transform;
            StartCoroutine(BossFightPrepare());
        }

        private IEnumerator BossFightPrepare()
        {
            GameManager.Instance.TimeScale = 0.1f;
            // playerScript.Freeze();
            CameraManager.Instance.ChangeTarget(CurrentBoss);
            yield return new WaitForSeconds(2f);
            BossFightStart();
        }

        public void BossFightStart()
        {
            GameManager.Instance.TimeScale = 1f;
            // playerScript.Thaw();
            CameraManager.Instance.ChangeTarget(GameManager.Instance.player);
            UIManager.Instance.BossFightUI(CurrentBoss.name);

            SoundManager.Instance.PlayBossTheme();
        }

        public void BossFightEnd()
        {
            UIManager.Instance.BossFightUIOff();

            SoundManager.Instance.PlayTheme();
        }

    }
}