using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Manager.Popups
{
    public class VictoryPopup : UIPanel
    {
        [SerializeField] private Image crown;
        [SerializeField] private RectTransform effect;
        [SerializeField] private List<RectTransform> trumpets = new();
        private readonly List<Tween> _tweens = new();

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            base.Open(onComplete, duration);
            Animate();
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

        private void Animate()
        {
            var t1 = transform.DOScale(Vector3.zero, .5f).From().SetEase(Ease.OutBack);
            foreach (var t2 in trumpets.Select(trumpet => trumpet.DOScale(Vector3.zero, 0.75f)
                         .From().SetEase(Ease.OutBack).SetDelay(0.25f)))
            {
                _tweens.Add(t2);
            }

            var t3 = crown.DOFade(0, 1f).From();
            var t4 = crown.transform.DOLocalMoveY(0, 1).From().SetEase(Ease.InOutBack);
            var t5 = effect.DOLocalRotate(new Vector3(0, 0, 360), 5, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetLoops(-1)
                .SetEase(Ease.Linear);

            _tweens.Add(t1);
            _tweens.Add(t3);
            _tweens.Add(t4);
            _tweens.Add(t5);
        }
    }
}