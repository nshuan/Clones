using System;
using System.Collections.Generic;
using Core.EndlessScroll;
using Core.InfiniteListView;
using Scripts.PlayerSettings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Home.SelectCharacter
{
    public class CharactersListView : InfiniteListView<CharacterPreviewInfo, CharacterPreviewData>
    {
        [FormerlySerializedAs("fullPreview")] [SerializeField] private CharacterPreview fullPreviewer;
        
        private void OnEnable()
        {
            // fullPreviewer.gameObject.SetActive(false);
            LoadCharactersData();
            OnDataLoaded();
        }

        public override void InitListViewData()
        {
            listView.data = new List<SimpleCell.ICellData>();
            for (var itemId = 0; itemId < elementInfos.Count; itemId++)
            {
                listView.data.Add(new CharacterPreviewData()
                {
                    ElementId = itemId,
                    ElementInfo = elementInfos[itemId],
                    Previewer = fullPreviewer
                });
            };
        }

        private void LoadCharactersData()
        {
            var characters = PlayerManager.Instance.AllCharacters;

            elementInfos = new List<CharacterPreviewInfo>();
            foreach (var character in characters)
            {
                elementInfos.Add(new CharacterPreviewInfo()
                {
                   CharacterData = character
                });
            }
        }
    }
}