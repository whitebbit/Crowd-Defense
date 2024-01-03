using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.FSM.Base;
using _3._Scripts.FSM.Interfaces;
using UnityEngine.XR;

namespace _3._Scripts.FSM
{
    public class StateMachine
    {
        private StateNode _current;
        private readonly Dictionary<Type, StateNode> _nodes = new();
        private readonly HashSet<ITransition> _anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            _current.State?.Update();
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.OnEnter();
        }

        private void ChangeState(IState state)
        {
            if (state == _current.State) return;

            var previousState = _current.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            _current = _nodes[state.GetType()];
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }
        
        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());
            
            if (node != null) return node;
            
            node = new StateNode(state);
            _nodes.Add(state.GetType(), node);

            return node;
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions.Where(transition => transition.Condition.Evaluate()))
            {
                return transition;
            }

            return _current.Transitions.FirstOrDefault(transition => transition.Condition.Evaluate());
        }
    }
}