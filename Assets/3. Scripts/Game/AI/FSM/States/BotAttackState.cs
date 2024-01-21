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
        private event Action OnDisable;
        private bool _attacked;
        public BotAttackState(Transform transform, Action onDisable = null)
        {
            _transform = transform;
            OnDisable += onDisable;
        }

        public override void OnEnter()
        {
            if (_attacked) return;
            
            LevelManager.Instance.CurrentLevel.BotAttacked();
            AttackPlayer();
            
            _transform.DOScale(Vector3.zero, 3).OnComplete(() => { OnDisable?.Invoke(); });;
            _transform.DOLocalMove(_transform.forward, 3);
            _attacked = true;
        }

        private void AttackPlayer()
        {
            HealthManager.HealthCount -= (int)_transform.localScale.x;
        }
    }
}