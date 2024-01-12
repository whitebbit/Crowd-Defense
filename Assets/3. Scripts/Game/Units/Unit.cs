using System;
using _3._Scripts.Game.Units.Health;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.Units
{
    public abstract class Unit: MonoBehaviour
    {
        public IDamageable Damageable { get; protected set; }
        public UnitHealth Health { get; protected set; }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnAwake(){}
        protected virtual void OnStart(){}
    }
}