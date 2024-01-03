namespace _3._Scripts.FSM.Interfaces
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}