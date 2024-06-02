using UnityEngine;

namespace Scripts.Home.Collections
{
    public class EnemyPreview : CollectionItemPreview<EnemyPreviewInfo>
    {
        [SerializeField] private EnemyInfoPreview infoPreview;
        
        protected override void ShowPreview(EnemyPreviewInfo info)
        {
            ShowVisual();
            ShowInfo();
        }

        private void ShowVisual()
        {
            
        }

        private void ShowInfo()
        {
            infoPreview.Setup(PreviewInfo.Stats);
        }
    }
}