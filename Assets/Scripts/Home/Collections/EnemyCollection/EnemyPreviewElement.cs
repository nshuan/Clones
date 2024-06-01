using Characters;
using Core.EndlessScroll;
using Core.InfiniteListView;
using EnemyCore.EnemyData;
using UnityEngine;

namespace Scripts.Home.Collections
{
    public class EnemyPreviewElement : CollectionElement<EnemyPreviewInfo>
    {
        public override void Setup(EnemyPreviewInfo participantInfo, int rank)
        {
            throw new System.NotImplementedException();
        }
    }

    public class EnemyPreviewInfo : CollectionElementInfo
    {
        public EnemyStats Stats { get; set; }
    }

    public class EnemyPreviewData : ElementData<EnemyPreviewInfo, EnemyPreviewElement>
    {
        public EnemyPreview Previewer { get; set; }
        
        protected override void Setup(EnemyPreviewElement cellView)
        {
            cellView.SetupPreviewer(Previewer);
            cellView.Setup(ElementInfo, ElementId);
        }
    }
}