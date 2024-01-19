using System;
using _3._Scripts.Game.Weapon.Animation;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Ballista
{
    public class BallistaAnimator : WeaponAnimator
    {
        [SerializeField] private Transform animationItem;
        [SerializeField] private Transform arrow;
        
        [Space] [SerializeField] private Vector3 endPosition;
        [SerializeField] private Vector3 endScale;
        private Tween _currentTween;

        private Vector3 _startPosition;
        private Vector3 _startScale;

        private void Start()
        {
            _startPosition = animationItem.localPosition;
            _startScale = animationItem.localScale;
        }

        public override Tween DoAnimation()
        {
            if (_currentTween != null) return null;

            arrow.gameObject.SetActive(false);
            _currentTween = animationItem.DOLocalMove(endPosition, 0.075f).OnComplete(() =>
            {
                animationItem.DOLocalMove(_startPosition, 0.075f).OnComplete(() =>
                {
                    Stop();
                    arrow.gameObject.SetActive(true);
                    arrow.DOScale(Vector3.zero, 0.075f).From();
                });
            });
            animationItem.DOScale(endScale, 0.075f).OnComplete(() =>
            {
                animationItem.DOScale(_startScale, 0.075f).OnComplete(Stop);
            });
            
            return _currentTween;
        }

        private void OnDisable()
        {
            Stop();
        }

        public override void Stop()
        {
            _currentTween?.Pause();
            _currentTween?.Kill();
            _currentTween = null;
        }
    }
}