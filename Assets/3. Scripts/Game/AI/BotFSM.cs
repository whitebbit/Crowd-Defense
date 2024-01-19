using System;
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
        public BotFSM(Transform transform, float speed, IAnimator animator, UnitHealth health, IDying dying, Action onDisable = null)
        {
            var idle = new BotIdleState(animator);
            var run = new BotRunState(transform, speed, animator);
            var death = new BotDeathState(dying);
            var attack = new BotAttackState(transform, onDisable);

            AddTransition(idle,
                new FuncPredicate(() => !LevelManager.Instance.CurrentLevel.LevelInProgress && health.Health > 0));
            AddTransition(run,
                new FuncPredicate(() => LevelManager.Instance.CurrentLevel.LevelInProgress &&
                                        health.Health > 0 && !run.OnFinish));
            AddTransition(death,
                new FuncPredicate(() => health.Health <= 0 && !death.IsDead));
            AddTransition(attack, new FuncPredicate(() => run.OnFinish));

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