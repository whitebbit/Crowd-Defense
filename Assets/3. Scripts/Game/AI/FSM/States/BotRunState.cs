using _3._Scripts.FSM.Base;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotRunState : State
    {
        private readonly AINavMeshAgent _navMeshAgent;

        public BotRunState(AINavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            var pos = _navMeshAgent.GetRandomTargetPosition(5, 25, 5, 25);
            _navMeshAgent.Start(pos);
        }

        public override void Update()
        {
            _navMeshAgent.OnMove();
        }

        public override void FixedUpdate()
        {
            
        }

        public override void OnExit()
        {
            base.OnExit();
            _navMeshAgent.Stop();
        }
    }
}