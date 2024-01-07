using System;
using _3._Scripts.Game.Units;
using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Damageable;
using _3._Scripts.Game.Units.Dying;
using _3._Scripts.Game.Units.Health;
using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Units.Scriptable.Animations;
using FSG.MeshAnimator;
using FSG.MeshAnimator.Snapshot;
using UnityEngine;


namespace _3._Scripts.Game.AI
{
    public class Bot : Unit
    {
        [SerializeField] private MeshAnimatorBase animator;
        [SerializeField] private MeshAnimationsHolder animations;
        private BotFSM _botFsm;
        private IAnimator _animator;

        protected override void OnAwake()
        {
            _animator = new BotAnimations(animator, animations);
            
        }

        private void Start()
        {
            _botFsm = new BotFSM(transform, _animator);
        }

        protected override void SetHealth()
        {
            var ragdoll = new Ragdoll(transform);
            var dying = new BotRagdollDying(ragdoll, _animator);

            Health = new UnitHealth(dying, 100);
        }

        protected override void SetDamageable()
        {
            Damageable = new UnitDamageable(Health);
        }

        private void Update()
        {
            if (Health.Health <= 0) return;

            _botFsm.Update();
        }

        private void FixedUpdate()
        {
            if (Health.Health <= 0) return;

            _botFsm.FixedUpdate();
        }
        
    }
}