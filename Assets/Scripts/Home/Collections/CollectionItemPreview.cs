using Core.InfiniteListView;
using UnityEngine;

namespace Scripts.Home.Collections
{
    public abstract class CollectionItemPreview<TInfo> : MonoBehaviour where TInfo : CollectionElementInfo
    {
        protected TInfo PreviewInfo;
        
        public virtual void SetupPreview(TInfo info)
        {
            PreviewInfo = info;
            ShowPreview(PreviewInfo);
        }

        protected abstract void ShowPreview(TInfo info);
    }
}