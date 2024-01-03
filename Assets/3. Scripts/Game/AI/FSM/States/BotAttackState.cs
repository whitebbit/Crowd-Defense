using _3._Scripts.FSM.Base;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotAttackState: State
    {
        private readonly Transform _transform;

        public BotAttackState(Transform transform)
        {
            _transform = transform;
        }

        public override void OnEnter()
        {
            base.OnEnter();
          
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