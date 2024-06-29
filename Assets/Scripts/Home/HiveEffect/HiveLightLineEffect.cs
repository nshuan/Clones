using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Home.HiveEffect
{
    public class HiveLightLineEffect : MonoBehaviour
    {
        [SerializeField] private float delayInSecond = 0f;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Light2D hiveLightPrefab;
        [SerializeField] private Transform beginHive;
        [SerializeField] private Transform endHive;

        private void OnEnable()
        {
            DoEffect();
        }

        private void DoEffect()
        {
            var lightLine = Instantiate(hiveLightPrefab);
            lightLine.transform.position = beginHive.position;
            lightLine.intensity = 0f;
            
            var sequence = DOTween.Sequence().AppendInterval(delayInSecond);

            sequence.AppendCallback(() => lightLine.intensity = 3f);
            sequence.Append(lightLine.transform.DOMove(endHive.position, duration).SetEase(Ease.InOutCubic));
            sequence.SetLoops(-1);

            sequence.Play();
        }
    }
}