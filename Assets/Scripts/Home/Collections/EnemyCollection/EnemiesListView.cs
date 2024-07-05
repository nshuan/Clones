using System;
using System.Collections.Generic;
using System.Linq;
using Core.EndlessScroll;
using Core.InfiniteListView;
using Scripts.Home.SelectCharacter;
using UnityEngine;

namespace Scripts.Home.Collections
{
    public class EnemiesListView : InfiniteListView<EnemyPreviewInfo, EnemyPreviewData>
    {
        [SerializeField] private EnemyPreview previewer;
        
        private void OnEnable()
        {
            LoadEnemyData();
            OnDataLoaded();

            EnemySearchField.OnEnemySearch += OnSearchEnemy;
        }

        private void OnDisable()
        {
            EnemySearchField.OnEnemySearch -= OnSearchEnemy;
        }

        public override void InitListViewData()
        {
            listView.data = new List<SimpleCell.ICellData>();
            foreach (var elementInfo in elementInfos)
            {
                listView.data.Add(new EnemyPreviewData()
                {
                    ElementInfo = elementInfo,
                    Previewer = previewer
                });
            };
        }

        private void OnSearchEnemy(string value)
        {
            listView.gameObject.SetActive(false);
            LoadEnemyData(value);
            listView.gameObject.SetActive(true);
            OnDataLoaded();
        }
        
        private void LoadEnemyData()
        {
            var enemies = EnemyManager.Instance.EnemyCollection.GetAllEnemy();

            elementInfos = new List<EnemyPreviewInfo>();
            foreach (var enemy in enemies)
            {
                elementInfos.Add(new EnemyPreviewInfo()
                {
                    Stats = enemy.EnemyPrefab.Stats
                });
            }
        }

        private void LoadEnemyData(string prefix)
        {
            var enemiesFull = EnemyManager.Instance.EnemyCollection.GetAllEnemy();
            var enemiesFound = enemiesFull
                .Where(e => e.EnemyPrefab.Stats.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
            
            elementInfos = new List<EnemyPreviewInfo>();
            foreach (var enemy in enemiesFound)
            {   
                elementInfos.Add(new EnemyPreviewInfo()
                {
                    Stats = enemy.EnemyPrefab.Stats
                });
            }
        }
    }
}

