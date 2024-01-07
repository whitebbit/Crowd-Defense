using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.Units.Dying
{
    public class BotRagdollDying: IDying
    {
        private readonly Ragdoll _ragdoll;
        private readonly IAnimator _animations;
        
        public BotRagdollDying(Ragdoll ragdoll, IAnimator animations)
        {
            _ragdoll = ragdoll;
            _animations = animations;
        }

        public void Dead()
        {
            _animations.PlayRandom("death");
            
            _ragdoll.RigidbodiesState(true);
            _ragdoll.CollidersState(false);
            
            Punch();
        }

        private void Punch()
        {
            var rigidbody = _ragdoll.Rigidbodies[Random.Range(0, _ragdoll.Rigidbodies.Count)];
            var randVector = new Vector3(Random.Range(-0.1f, 0.1f), 1, 0);
            var randTorqueVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            var randForce = Random.Range(10, 25);
            var randTorqueForce = Random.Range(100, 200);
            
            rigidbody.AddForce(randVector * randForce, ForceMode.Impulse);
            rigidbody.AddTorque(randTorqueVector * randTorqueForce, ForceMode.Impulse);
        }
    }
}