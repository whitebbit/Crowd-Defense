using _3._Scripts.Architecture.Extensions;
using _3._Scripts.Architecture.Helpers;
using _3._Scripts.Architecture.Scriptable.Animations;
using _3._Scripts.FSM;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game.AI.FSM.States;
using _3._Scripts.TweenAnimations;
using UnityEngine;
using UnityEngine.AI;

namespace _3._Scripts.Game.AI
{
    public class BotFSM : FSMHandler
    {
        private readonly OverlapFounder _overlapFounder;
        private float Distance => _overlapFounder.DistanceToObject();

        public BotFSM(AnimationObject botFearAnimation, OverlapFounder overlapFounder,
            NavMeshAgent navMeshAgent)
        {
            _overlapFounder = overlapFounder;

            var idle = new BotIdleState();
            var aiNavMesh = new AINavMeshAgent(navMeshAgent, () => StateMachine.ChangeState(idle));
            var fear = new BotFearState(botFearAnimation);
            var run = new BotRunState(aiNavMesh);

            AddTransition(idle,
                new FuncPredicate(() => !overlapFounder.ObjectsInRadius() && !aiNavMesh.Moving));
            AddTransition(fear,
	            new FuncPredicate(() => Distance is <= 7f aand > 3f && !aiNavMesh.Moving));
            AddTransition(run,
                new FuncPredicate(() => Distance <= 3f && !aiNavMesh.Moving));

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