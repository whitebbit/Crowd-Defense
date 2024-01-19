using System;
using _3._Scripts.Game.Weapon.Animation;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.MachineGun
{
    public class MachineGunAnimator : WeaponAnimator
    {
        [SerializeField] private Transform rotateItem;

        private Tween _currentTween;

        public override Tween DoAnimation()
        {
            if (_currentTween != null) return null;

            var eulerAngles = rotateItem.localEulerAngles;
            _currentTween = rotateItem.DOLocalRotate(new Vector3(eulerAngles.x, eulerAngles.y, 360), 0.25f,
                    RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetLoops(-1);

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