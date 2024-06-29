using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EasyButtons;
using Scripts.Home.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Home
{
    public class HomeNavigator : MonoBehaviour
    {
        [SerializeField] private Transform boardHolder;
        [SerializeField] private Transform title;
        [SerializeField] private Image preview;
        private List<HomeMenuButton> _homeButtons;
        private List<Vector3> _homeButtonsOriginalPos;
        private float _previewAlpha;
        
        private HomeMenuButton _currentSelect;
        
        private void Awake()
        {
            _homeButtons = GetComponentsInChildren<HomeMenuButton>().ToList();
            _homeButtonsOriginalPos = new List<Vector3>();

            HomeMenuButton.OnButtonClick += OnMenuButtonClick;
        }

        private void OnDestroy()
        {
            HomeMenuButton.OnButtonClick -= OnMenuButtonClick;
        }

        [Button]
        private Tween DoShowEffect()
        {
            var sequence = DOTween.Sequence();

            _previewAlpha = preview.color.a;
            sequence.Append(preview.DOColor(preview.color - new Color(0f, 0f, 0f, _previewAlpha), 0.3f));
            sequence.Join(title.DOScale(0.5f, 0.3f).SetEase(Ease.OutSine));
            foreach (var button in _homeButtons)
            {
                _homeButtonsOriginalPos.Add(button.transform.localPosition);
                sequence.AppendCallback(() =>
                {
                    button.transform.DOLocalMoveX(button.transform.localPosition.x - 120, 0.2f);
                });
                sequence.AppendInterval(0.1f);
            }

            return sequence;
        }

        [Button]
        private Tween DoHideEffect()
        {
            var sequence = DOTween.Sequence();

            for (var i = 0; i < _homeButtons.Count; i++)
            {
                var button = _homeButtons[i];
                var oriPos = _homeButtonsOriginalPos[i];
                sequence.AppendCallback(() =>
                {
                    button.transform.DOLocalMoveX(oriPos.x, 0.2f);
                });
                sequence.AppendInterval(0.1f);
            }

            sequence.Append(title.DOScale(1f, 0.3f).SetEase(Ease.OutSine));
            sequence.Join(preview.DOColor(preview.color + new Color(0f, 0f, 0f, _previewAlpha), 0.3f));

            return sequence;
        }

        private void OnMenuButtonClick(HomeMenuButton button)
        {
            if (_currentSelect is not null)
            {
                _currentSelect.HidePopup();
                _currentSelect = button;
                _currentSelect.OnShowPopup(boardHolder);
            }
            else
            {
                _currentSelect = button;
                DOTween.Sequence()
                    .Append(DoShowEffect())
                    .Append(button.DoClickEffect())
                    .Play();
            }
        }

        public void OnClosePopup()
        {
            if (_currentSelect is null)
            {
                DoHideEffect().Play();
                return;
            }

            DOTween.Sequence()
                .Append(_currentSelect.DoHideEffect())
                .Join(DoHideEffect())
                .Play();
            _currentSelect = null;
        }
    }
}