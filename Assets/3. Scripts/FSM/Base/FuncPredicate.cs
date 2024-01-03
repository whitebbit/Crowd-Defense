using System;
using _3._Scripts.FSM.Interfaces;

namespace _3._Scripts.FSM.Base
{
    public class FuncPredicate: IPredicate
    {
        private readonly Func<bool> _func;

        public FuncPredicate(Func<bool> func)
        {
            _func = func;
        }
        public bool Evaluate() => _func.Invoke();
    }
}