using _3._Scripts.FSM.Base;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotRunState : State
    {
        private Transform _transform;

        public BotRunState(Transform transform)
        {
            _transform = transform;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void Update()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _transform.forward, 1 * Time.deltaTime);
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