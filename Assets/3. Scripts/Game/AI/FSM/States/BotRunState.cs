using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotRunState : State
    {
        private readonly Transform _transform;
        private readonly IAnimator _animator;
        public BotRunState(Transform transform, IAnimator animator)
        {
            _transform = transform;
            _animator = animator;
        }

        public override void OnEnter()
        {
            _animator.Play("walk");
        }

        public override void Update()
        {
            _transform.position =
                Vector3.MoveTowards(_transform.position, _transform.forward * 1000000, 1 * Time.deltaTime);
        }

        public override void FixedUpdate()
        {
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}