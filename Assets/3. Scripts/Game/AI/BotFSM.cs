using _3._Scripts.FSM.Base;
using _3._Scripts.Game.AI.FSM.States;
using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.AI
{
    public class BotFSM : FSMHandler
    {
        public BotFSM(Transform transform, IAnimator animator)
        {
            var idle = new BotIdleState(transform, animator);
            var run = new BotRunState(transform, animator);
            var death = new BotDeathState(transform);
            var attack = new BotAttackState(transform);


            AddTransition(idle,
                new FuncPredicate(() => false));

            AddTransition(run,
                new FuncPredicate(() => false));

            AddTransition(death,
                new FuncPredicate(() => Input.GetKeyDown(KeyCode.D)));

            AddTransition(attack,
                new FuncPredicate(() => Input.GetKeyDown(KeyCode.A)));

            StateMachine.SetState(run);
        }

        public void Update()
        {
            StateMachine.Update();
        }

        public void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }
    }
}