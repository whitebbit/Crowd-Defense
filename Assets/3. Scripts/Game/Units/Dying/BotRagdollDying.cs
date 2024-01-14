using System;
using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3._Scripts.Game.Units.Dying
{
    public class BotRagdollDying: IDying
    {
        private readonly Ragdoll _ragdoll;
        private readonly IAnimator _animator;
        private readonly bool _punchOnDeath;
        private event Action OnDead;

        public BotRagdollDying(Ragdoll ragdoll, IAnimator animator, bool punchOnDeath = true, Action onDead = null)
        {
            _ragdoll = ragdoll;
            _animator = animator;

            _punchOnDeath = punchOnDeath;
            OnDead += onDead;
        }

        public void Dead()
        {
            _animator.PlayRandom("death");

            if (_punchOnDeath)
            {
                _ragdoll.RigidbodiesState(true);
                Punch();
            }
            
            _ragdoll.CollidersState(false);
            OnDead?.Invoke();
        }

        private void Punch()
        {
            var rigidbody = _ragdoll.Rigidbodies[Random.Range(0, _ragdoll.Rigidbodies.Count)];
            var randVector = new Vector3(Random.Range(-0.5f, 0.5f), 1, 0);
            var randTorqueVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            var randForce = Random.Range(10, 20);
            var randTorqueForce = Random.Range(25, 50);
            
            rigidbody.AddForce(randVector * randForce, ForceMode.Impulse);
            rigidbody.AddTorque(randTorqueVector * randTorqueForce, ForceMode.Impulse);
             
        }
    }
}