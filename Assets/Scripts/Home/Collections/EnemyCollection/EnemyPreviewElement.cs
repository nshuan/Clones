using Characters;
using Core.EndlessScroll;
using Core.InfiniteListView;
using EnemyCore.EnemyData;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Home.Collections
{
    public class EnemyPreviewElement : CollectionElement<EnemyPreviewInfo>
    {
        [SerializeField] private Image elementImage;
        
        public override void Setup(EnemyPreviewInfo elementInfo, int rank)
        {
            ElementInfo = elementInfo;
            elementImage.color = elementInfo.Stats.VisualColor;
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