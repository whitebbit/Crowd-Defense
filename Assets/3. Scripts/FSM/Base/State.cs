using _3._Scripts.FSM.Interfaces;
using UnityEngine;

namespace _3._Scripts.FSM.Base
{
    public abstract class State: IState
    {
        
        public virtual void OnEnter()
        {
            //Debug.Log($"{GetType()} - OnEnter");
        }

        public virtual void Update()
        {
           
        }

        public virtual void FixedUpdate()
        {
           
        }

        public virtual void OnExit()
        {
            //Debug.Log($"{GetType()} - OnExit");
        }
    }
}