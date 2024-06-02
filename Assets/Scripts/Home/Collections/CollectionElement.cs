using Core.EndlessScroll;
using Core.InfiniteListView;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

namespace Scripts.Home.Collections
{
    public abstract class CollectionElement<TInfo> : ListViewElement<TInfo>, SimpleCell.ICellView, IPointerClickHandler where TInfo : CollectionElementInfo
    {
        protected CollectionItemPreview<TInfo> Previewer;
        protected TInfo ElementInfo; 
        
        public void SetupPreviewer(CollectionItemPreview<TInfo> target)
        {
            Previewer = target;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Previewer.SetupPreview(ElementInfo);
            if (Previewer.gameObject.activeInHierarchy == false) Previewer.gameObject.SetActive(true);
        }
    }

    public class CollectionElementInfo : IElementInfo
    {
        
    }
}

