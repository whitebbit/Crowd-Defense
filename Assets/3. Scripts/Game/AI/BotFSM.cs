using _3._Scripts.FSM.Base;
using _3._Scripts.Game.AI.FSM.States;
using UnityEngine;


namespace _3._Scripts.Game.AI
{
    public class BotFSM : FSMHandler
    {
        public BotFSM(Transform transform)
        {
            var run = new BotRunState(transform);
            
            AddTransition(run,
                new FuncPredicate(() => true));

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