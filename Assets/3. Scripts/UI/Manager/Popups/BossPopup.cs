using System;
using System.Collections.Generic;
using DG.Tweening;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Manager.Popups
{
    public class BossPopup : UIPanel
    {
        [SerializeField] private RectTransform firstText;
        [SerializeField] private RectTransform secondText;
        [SerializeField] private Image frame;
        [SerializeField] private Image bossIcon;

        private readonly List<Tween> _tweens = new();

        private void Start()
        {
            Initialize();
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            AnimateFrame();
            AnimateText();
            AnimateIcon();

            base.Open(onComplete, duration);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            base.Close(() =>
            {
                foreach (var tween in _tweens)
                {
                    tween?.Pause();
                    tween?.Kill();
                }

                _tweens.Clear();

                onComplete?.Invoke();
            }, duration);
        }

        private void AnimateFrame()
        {
            var tween = frame.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

            _tweens.Add(tween);
        }

        private void AnimateText()
        {
            var tween1 = firstText.DOLocalMoveX(firstText.transform.localPosition.x + 500, 2)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            var tween2 = secondText.DOLocalMoveX(secondText.transform.localPosition.x - 500, 2)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

            _tweens.Add(tween1);
            _tweens.Add(tween2);
        }

        private void AnimateIcon()
        {
            var tween1 = bossIcon.DOFade(0, 0.75f).From();

            var tween2 = bossIcon.transform.DOScale(0, 1f)
                .From()
                .SetEase(Ease.InOutBack);
            _tweens.Add(tween1);
            _tweens.Add(tween2);
        }
    }
}