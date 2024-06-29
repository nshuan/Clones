using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Home.Collections
{
    public class EnemyPreview : CollectionItemPreview<EnemyPreviewInfo>
    {
        [SerializeField] private EnemyInfoPreview infoPreview;
        [SerializeField] private Image bigImage;

        private void OnEnable()
        {
            InitPreview();
        }

        protected override void InitPreview()
        {
            bigImage.gameObject.SetActive(false);    
            infoPreview.HideData();
        }

        protected override void ShowPreview(EnemyPreviewInfo info)
        {
            ShowVisual();
            ShowInfo();
        }

        private void ShowVisual()
        {
            bigImage.gameObject.SetActive(true);   
            bigImage.color = PreviewInfo.Stats.VisualColor;
            bigImage.transform.localScale = PreviewInfo.Stats.SizeScale * Vector3.one;
        }

        private void ShowInfo()
        {
            infoPreview.ShowData();
            infoPreview.Setup(PreviewInfo.Stats);
        }
    }
}