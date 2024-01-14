using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Main;
using _3._Scripts.Game.Units.Health;
using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.UI.Manager;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotAttackState : State
    {
        private readonly Transform _transform;
        private readonly IAnimator _animator;
        private event Action OnDisable;

        public BotAttackState(Transform transform, IAnimator animator, Action onDisable = null)
        {
            _transform = transform;
            _animator = animator;
            OnDisable += onDisable;
        }

        public override void OnEnter()
        {
            //_animator.PlayRandom("attack");
            LevelManager.Instance.CurrentLevel.BotAttacked();
            AttackPlayer();
            _transform.DOScale(Vector3.zero, 1f).SetDelay(1f).OnComplete(() => { OnDisable?.Invoke(); });
        }

        private void AttackPlayer()
        {
            HealthManager.HealthCount -= 1 * (int)_transform.localScale.x;
        }
    }
}