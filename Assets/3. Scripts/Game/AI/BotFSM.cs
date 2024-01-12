using _3._Scripts.FSM.Base;
using _3._Scripts.Game.AI.FSM.States;
using _3._Scripts.Game.Main;
using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Health;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.AI
{
    public class BotFSM : FSMHandler
    {
        public BotFSM(Transform transform, IAnimator animator, UnitHealth health, IDying dying)
        {
            var idle = new BotIdleState(transform, animator);
            var run = new BotRunState(transform, animator);
            var death = new BotDeathState(transform, animator, dying);
            var attack = new BotAttackState(transform);
            
            AddTransition(idle,
                new FuncPredicate(() => !Level.Instance.LevelInProgress && health.Health > 0));

            AddTransition(run,
                new FuncPredicate(() => Level.Instance.LevelInProgress && health.Health > 0));

            AddTransition(death,
                new FuncPredicate(() => health.Health <= 0 && !death.IsDead));

            AddTransition(attack,
                new FuncPredicate(() => Input.GetKeyDown(KeyCode.A)));

            StateMachine.SetState(idle);
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