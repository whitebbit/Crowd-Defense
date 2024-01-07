using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotDeathState: State
    {
        private readonly Transform _transform;
        private readonly IAnimator _animator;
        private readonly IDying _dying;

        public bool IsDead { get; private set; }

        public BotDeathState(Transform transform, IAnimator animator, IDying dying)
        {
            _transform = transform;
            _animator = animator;
            _dying = dying;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            IsDead = true;
            _dying.Dead();
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