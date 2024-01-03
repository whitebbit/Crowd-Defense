using _3._Scripts.FSM.Interfaces;

namespace _3._Scripts.FSM.Base
{
    public abstract class FSMHandler
    {
        protected readonly StateMachine StateMachine = new();
        
        protected void AddTransition(IState from, IState to, IPredicate condition) =>
            StateMachine.AddTransition(from, to, condition);
        
        protected void AddTransition(IState to, IPredicate condition) =>
            StateMachine.AddAnyTransition(to, condition);
    }
}