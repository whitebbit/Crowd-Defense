using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Main;
using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotRunState : State
    {
        private readonly Transform _transform;
        private readonly IAnimator _animator;
        private readonly float _speed;

        public bool OnFinish => Vector3.Distance(_transform.position,
            LevelManager.Instance.CurrentLevel.Player.transform.position) <= 13;

        public BotRunState(Transform transform, float speed, IAnimator animator)
        {
            _transform = transform;
            _animator = animator;
            _speed = speed;
        }

        public override void OnEnter()
        {
            _animator.Play("walk");
        }

        public override void Update()
        {
            _transform.position =
                Vector3.MoveTowards(_transform.position, _transform.forward * 1000000, _speed * Time.deltaTime);
        }
    }
}