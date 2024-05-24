using System.Collections.Generic;
using Core.EndlessScroll;
using UnityEngine;
using UnityEngine.UI;

namespace Core.InfiniteListView
{
    public interface IInfiniteListView
    {
        void InitListViewData();
        void OnDataLoaded();
    }
    
    public abstract class InfiniteListView<TInfo, TElementData> : MonoBehaviour, IInfiniteListView 
        where TInfo : IElementInfo
        where TElementData : ISetupElement<TInfo>, SimpleCell.ICellData, new()
    {
        [SerializeField] protected ScrollRect scrollRect;
        [SerializeField] protected SimpleListView listView;

        protected List<TInfo> elementInfos;
        
        public virtual void InitListViewData()
        {
            //set up list ranking with rank > TopRankAmount
            listView.data = new List<SimpleCell.ICellData>();
            for (var itemId = 0; itemId < elementInfos.Count; itemId++)
            {
                listView.data.Add(new TElementData()
                {
                    ElementId = itemId,
                    ElementInfo = elementInfos[itemId]
                });
            }
        }

        public virtual void OnDataLoaded()
        {
            listView.gameObject.SetActive(false);
            InitListViewData();
            listView.gameObject.SetActive(true);
            listView.Initialize();
            ResetScrollView();
        }
        
        protected void ResetScrollView()
        {
            scrollRect.verticalNormalizedPosition = 1;
        }
    }
}