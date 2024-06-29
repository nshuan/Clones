using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Home.HiveEffect
{
    public class HiveClosedLineEffect : MonoBehaviour
    {
        [SerializeField] private float delayInSecond = 0f;
        [SerializeField] private float delayBetweenPath = 0.5f;
        [SerializeField] private float pathDuration = 1.5f;
        [SerializeField] private Light2D hiveLightPrefab;
        [SerializeField] private List<Transform> anchors;

        private void OnEnable()
        {
            if (anchors.Count < 2) return;
            DoEffect();
        }

        private void DoEffect()
        {
            var lightLine = Instantiate(hiveLightPrefab);
            lightLine.transform.position = anchors[0].position;
            lightLine.intensity = 0f;
            
            var sequence = DOTween.Sequence().AppendInterval(delayInSecond);

            sequence.Append(DoOnLight(lightLine, 3f, 0.5f));
            for (int i = 1; i < anchors.Count; i++)
            {
                sequence.Append(lightLine.transform.DOMove(anchors[i].position, pathDuration).SetEase(Ease.InOutCubic));
                sequence.AppendInterval(delayBetweenPath);
            }
            sequence.Append(lightLine.transform.DOMove(anchors[0].position, pathDuration).SetEase(Ease.InOutCubic));
            sequence.Append(DoOnLight(lightLine, 0f, 0.5f));

            sequence.SetLoops(-1);
            sequence.Play();
        }

        private Tween DoOnLight(Light2D li, float targetIntensity, float duration)
        {
            var sequence = DOTween.Sequence();

            var timeStep = duration / 32;
            var intensityStep = (targetIntensity - li.intensity) / 32;
            
            for (var i = 0; i < 32; i++)
            {
                sequence.AppendCallback(() => li.intensity += intensityStep);
                sequence.AppendInterval(timeStep);
            }

            sequence.AppendCallback(() => li.intensity = targetIntensity);

            return sequence;
        }
    }
}