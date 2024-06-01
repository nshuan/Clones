using Core.EndlessScroll;
using UnityEngine;

namespace Core.InfiniteListView
{
    public abstract class ListViewElement<T> : MonoBehaviour where T : IElementInfo
    {
        public abstract void Setup(T elementInfo, int rank);
    }

    public abstract class ElementData<TInfo, TElement> 
        : SimpleCell.SimpleCellData<TElement>, ISetupElement<TInfo>
        where TInfo : IElementInfo
        where TElement : ListViewElement<TInfo>, SimpleCell.ICellView
    {
        public int ElementId { get; set; }
        public TInfo ElementInfo { get; set; }
        
        protected override void Setup(TElement cellView)
        {
            cellView.Setup(ElementInfo, ElementId);
        }
    }
}