using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotIdleState: State
    {
        private readonly Transform _transform;
        private readonly IAnimator _animator;

        public BotIdleState(Transform transform, IAnimator animator)
        {
            _transform = transform;
            _animator = animator;
        }

        public override void OnEnter()
        {
            _animator.Play("idle");
        }

        public override void Update()
        {

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