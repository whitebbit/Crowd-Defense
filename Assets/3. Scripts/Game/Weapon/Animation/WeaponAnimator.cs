using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Animation
{
    public abstract class WeaponAnimator: MonoBehaviour
    {
        public abstract Tween DoAnimation();

        public abstract void Stop();
    }
}